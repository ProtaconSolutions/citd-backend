using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Citd.Roslyn
{
    public class Compiler
    {
        public Assembly Compile(string code)
        {
            using (var stream = new MemoryStream())
            {
                var result = CSharpCompilation
                    .Create(Path.GetRandomFileName())
                    .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    .AddReferences(
                        MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                        MetadataReference.CreateFromFile(typeof(Enumerable).AssemblyQualifiedName)
                    )
                    .AddSyntaxTrees(CSharpSyntaxTree.ParseText(code))
                    .Emit(stream);

                if (!result.Success)
                {
                    var errorMsg = result.Diagnostics
                        .Where(
                            diasnostic => diasnostic.IsWarningAsError || diasnostic.Severity == DiagnosticSeverity.Error)
                        .Select(failure => $"{failure.Id}: {failure.GetMessage()}")
                        .Aggregate((all, next) => $"{all}{Environment.NewLine}{next}");

                    throw new InvalidOperationException($"Build failed with errors{Environment.NewLine}{errorMsg}");
                }

                stream.Seek(0, SeekOrigin.Begin);
                return AssemblyLoadContext.Default.LoadFromStream(stream);
            }
        }
    }
}
FROM microsoft/dotnet:1.1.1-runtime

WORKDIR /dotnetapp

COPY out .

ENTRYPOINT ["dotnet", "dotnetapp.dll"]

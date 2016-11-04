using System;

public static class WarmUpExtension
{
    public static IServiceProvider WarmUp<T>(this IServiceProvider app)
    {
        app.GetService(typeof(T));
        return app;
    }

}
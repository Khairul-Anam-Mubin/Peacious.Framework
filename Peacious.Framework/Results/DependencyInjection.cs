using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ErrorAdapters;
using Peacious.Framework.Results.ErrorFactories;
using Peacious.Framework.Results.ResultAdapters;
using Peacious.Framework.Results.ResultFactories;

namespace Peacious.Framework.Results;

public static class DependencyInjection
{
    public static IServiceCollection AddErrorActionResultAdapters(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IErrorActionResultAdapter, DefaultErrorActionResultAdapter>(ErrorResponseType.Default);
        services.AddKeyedSingleton<IErrorActionResultAdapter, ProblemDetailsErrorActionResultAdapter>(ErrorResponseType.ProblemDetails);
        services.AddSingleton<IErrorActionResultAdapterFactory, ErrorActionResultAdapterFactory>();
        return services;
    }

    public static IServiceCollection AddActionResultAdapters(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IActionResultAdapter, DefaultActionResultAdapter>(ResponseType.Default);
        services.AddSingleton<IActionResultAdapterFactory, ActionResultAdapterFactory>();
        return services;
    }

    public static IServiceCollection AddCustomErrorActionResultAdapters(this IServiceCollection services)
    {
        services.AddSingleton(CustomErrorActionResultAdapterFactory.Instance);
        return services;
    }

    public static IServiceCollection AddCustomActionResultAdapters(this IServiceCollection services)
    {
        services.AddSingleton(CustomActionResultAdapterFactory.Instance);
        return services;
    }
}

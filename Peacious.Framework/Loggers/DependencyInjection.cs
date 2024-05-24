using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Extensions;

namespace Peacious.Framework.Loggers;

public static class DependencyInjection
{
    public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.TryGetConfig<LoggingConfig>("LoggingConfig"));
        services.AddTransient<ConsoleLogger>();
        services.AddTransient<FileLogger>();
        services.AddTransient<DbLogger>();
        services.AddTransient<ILoggerChainProvider, LoggerChainProvider>();
        services.AddSingleton<ILogger, Logger>();
        return services;
    }
}
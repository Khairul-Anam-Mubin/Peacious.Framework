using Microsoft.Extensions.Configuration;

namespace Peacious.Framework.Extensions;

public static class ConfigurationExtension
{
    public static T? GetConfig<T>(this IConfiguration configuration, string key)
    {
        var config = configuration.GetSection(key).Get<T>();

        return config is null ? DynamicConfig.Instance.GetConfig<T>(key) : config;
    }

    public static T TryGetConfig<T>(this IConfiguration configuration, string key)
    {
        return configuration.GetConfig<T>(key) ?? throw new Exception("Config null");
    }
}
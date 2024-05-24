using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Cache.DistributedCache;

namespace Peacious.Framework.PubSub;

public static class DependencyInjection
{
    public static IServiceCollection AddPubSub(this IServiceCollection services)
    {
        services.AddSingleton<IRedisClientManager, RedisClientManager>();
        services.AddSingleton<IPubSub, RedisPubSub>();
        return services;
    }
}
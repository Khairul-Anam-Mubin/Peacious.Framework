using Microsoft.Extensions.Configuration;
using Peacious.Framework.Cache.DistributedCache;
using Peacious.Framework.Extensions;
using Peacious.Framework.ORM;

namespace Peacious.Framework.PubSub;

public class RedisPubSub : IPubSub
{
    private readonly IRedisClientManager _redisClientManager;
    private readonly DatabaseInfo _databaseInfo;

    public RedisPubSub(IRedisClientManager redisClientManager, IConfiguration configuration)
    {
        _redisClientManager = redisClientManager;
        _databaseInfo = configuration.TryGetConfig<DatabaseInfo>("RedisConfig");
    }

    public async Task PublishAsync<T>(string channel, T message)
    {
        await _redisClientManager.GetConnectionMultiplexer(_databaseInfo)
            .GetSubscriber()
            .PublishAsync(channel, message.Serialize());
    }

    public async Task PublishAsync(string channel, PubSubMessage message)
    {
        await PublishAsync<PubSubMessage>(channel, message);
    }

    public async Task SubscribeAsync<T>(string channel, Action<string, T?> handler)
    {
        await _redisClientManager.GetConnectionMultiplexer(_databaseInfo)
            .GetSubscriber()
            .SubscribeAsync(channel, (redisChannel, message) =>
            {
                var data = message.SmartCast<T>();
                handler(channel, data!);
            });
    }
}
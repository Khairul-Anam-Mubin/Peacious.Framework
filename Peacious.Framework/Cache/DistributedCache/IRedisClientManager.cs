using Peacious.Framework.ORM;
using StackExchange.Redis;

namespace Peacious.Framework.Cache.DistributedCache;

public interface IRedisClientManager
{
    ConnectionMultiplexer GetConnectionMultiplexer(DatabaseInfo databaseInfo);
}
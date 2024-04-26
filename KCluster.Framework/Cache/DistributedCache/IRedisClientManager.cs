using KCluster.Framework.ORM;
using StackExchange.Redis;

namespace KCluster.Framework.Cache.DistributedCache;

public interface IRedisClientManager
{
    ConnectionMultiplexer GetConnectionMultiplexer(DatabaseInfo databaseInfo);
}
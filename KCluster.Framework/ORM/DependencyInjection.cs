using KCluster.Framework.Cache.DistributedCache;
using KCluster.Framework.ORM.Factories;
using KCluster.Framework.ORM.Interfaces;
using KCluster.Framework.ORM.MongoDb;
using KCluster.Framework.ORM.Sql;
using KCluster.Framework.ORM.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace KCluster.Framework.ORM;

public static class DependencyInjection
{
    public static IServiceCollection AddSqlDb(this IServiceCollection services)
    {
        services.AddSingleton<ISqlClientManager, SqlClientManager>();
        services.AddSingleton<SqlDbContext>();
        services.AddTransient<IDbContextFactory, DbContextFactory>();

        return services;
    }

    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClientManager, MongoClientManager>();
        services.AddTransient<IIndexManager, MongoDbIndexManager>();
        services.AddSingleton<MongoDbContext>();
        services.AddTransient<IDbContextFactory, DbContextFactory>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        services.AddSingleton<IRedisClientManager, RedisClientManager>();
        services.AddSingleton<IDistributedCache, RedisCache>();
        services.AddTransient<IDbContextFactory, DbContextFactory>();
        return services;
    }
}
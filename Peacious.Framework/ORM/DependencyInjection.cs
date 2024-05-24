using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Cache.DistributedCache;
using Peacious.Framework.ORM.Factories;
using Peacious.Framework.ORM.Interfaces;
using Peacious.Framework.ORM.MongoDb;
using Peacious.Framework.ORM.Sql;
using Peacious.Framework.ORM.UnitOfWorks;

namespace Peacious.Framework.ORM;

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
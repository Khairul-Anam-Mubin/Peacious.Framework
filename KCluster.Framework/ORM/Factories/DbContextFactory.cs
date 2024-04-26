using KCluster.Framework.ORM.Enums;
using KCluster.Framework.ORM.Interfaces;
using KCluster.Framework.ORM.MongoDb;
using KCluster.Framework.ORM.Sql;
using Microsoft.Extensions.DependencyInjection;

namespace KCluster.Framework.ORM.Factories;

public class DbContextFactory : IDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDbContext GetDbContext(Context context)
    {
        return context switch
        {
            Context.Mongo => _serviceProvider.GetRequiredService<MongoDbContext>(),
            Context.Sql => _serviceProvider.GetRequiredService<SqlDbContext>(),
            _ => _serviceProvider.GetRequiredService<MongoDbContext>()
        };
    }
}
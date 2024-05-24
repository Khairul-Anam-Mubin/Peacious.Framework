using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.ORM.Enums;
using Peacious.Framework.ORM.Interfaces;
using Peacious.Framework.ORM.MongoDb;
using Peacious.Framework.ORM.Sql;

namespace Peacious.Framework.ORM.Factories;

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
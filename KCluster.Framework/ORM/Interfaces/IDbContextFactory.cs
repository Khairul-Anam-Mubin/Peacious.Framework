using KCluster.Framework.ORM.Enums;

namespace KCluster.Framework.ORM.Interfaces;

public interface IDbContextFactory
{
    IDbContext GetDbContext(Context context);
}
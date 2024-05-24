using Peacious.Framework.ORM.Enums;

namespace Peacious.Framework.ORM.Interfaces;

public interface IDbContextFactory
{
    IDbContext GetDbContext(Context context);
}
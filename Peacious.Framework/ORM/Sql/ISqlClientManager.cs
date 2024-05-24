using System.Data;

namespace Peacious.Framework.ORM.Sql;

public interface ISqlClientManager
{
    IDbConnection CreateConnection(DatabaseInfo databaseInfo);
}
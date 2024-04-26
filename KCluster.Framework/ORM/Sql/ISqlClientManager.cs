using System.Data;

namespace KCluster.Framework.ORM.Sql;

public interface ISqlClientManager
{
    IDbConnection CreateConnection(DatabaseInfo databaseInfo);
}
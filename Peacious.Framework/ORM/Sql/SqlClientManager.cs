using System.Data;
using Microsoft.Data.SqlClient;

namespace Peacious.Framework.ORM.Sql;

public class SqlClientManager : ISqlClientManager
{
    public IDbConnection CreateConnection(DatabaseInfo databaseInfo)
    {
        return new SqlConnection(databaseInfo.ConnectionString);
    }
}
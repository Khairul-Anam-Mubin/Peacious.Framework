using MongoDB.Driver;

namespace KCluster.Framework.ORM.MongoDb;

public interface IMongoClientManager
{
    MongoClient GetClient(DatabaseInfo databaseInfo);

    IMongoDatabase GetDatabase(DatabaseInfo databaseInfo);

    IMongoCollection<T> GetCollection<T>(DatabaseInfo databaseInfo);
}
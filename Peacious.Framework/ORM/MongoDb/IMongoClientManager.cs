using MongoDB.Driver;
using Peacious.Framework.ORM.Interfaces;

namespace Peacious.Framework.ORM.MongoDb;

public interface IMongoClientManager
{
    MongoClient GetClient(DatabaseInfo databaseInfo);

    IMongoDatabase GetDatabase(DatabaseInfo databaseInfo);

    IMongoCollection<T> GetCollection<T>(DatabaseInfo databaseInfo) where T : class, IRepositoryItem;
}
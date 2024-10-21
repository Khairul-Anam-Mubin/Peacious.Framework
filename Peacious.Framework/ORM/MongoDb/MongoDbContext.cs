using MongoDB.Driver;
using Peacious.Framework.Extensions;
using Peacious.Framework.ORM.Builders;
using Peacious.Framework.ORM.Interfaces;
using Peacious.Framework.ORM.MongoDb.Composers;

namespace Peacious.Framework.ORM.MongoDb;

public class MongoDbContext : IDbContext
{
    private readonly IMongoClientManager _mongoClientManager;

    public MongoDbContext(IMongoClientManager mongoClientManager)
    {
        _mongoClientManager = mongoClientManager;
    }

    public async Task<bool> SaveAsync<T>(DatabaseInfo databaseInfo, T item) where T : class, IRepositoryItem
    {
        try
        {
            var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

            var filter = Builders<T>.Filter.Eq(o => o.Id, item.Id);

            await collection.ReplaceOneAsync(filter, item, new ReplaceOptions { IsUpsert = true });

            Console.WriteLine($"Successfully Save Item : {item.Serialize()}\n");

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem Save Item : {item.Serialize()}\n{ex.Message}\n");

            return false;
        }
    }

    public async Task<bool> SaveManyAsync<T>(DatabaseInfo databaseInfo, List<T> items) where T : class, IRepositoryItem
    {
        if (items.Count == 0) return false;

        var writeModels = new List<WriteModel<T>>();

        foreach (var item in items)
        {
            var filter = Builders<T>.Filter.Eq(o => o.Id, item.Id);

            var replaceOneModel = new ReplaceOneModel<T>(filter, item)
            {
                IsUpsert = true
            };

            writeModels.Add(replaceOneModel);
        }

        var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

        var writeResult = await collection.BulkWriteAsync(writeModels);

        return writeResult is { IsAcknowledged: true };
    }

    public async Task<bool> DeleteOneByIdAsync<T>(DatabaseInfo databaseInfo, string id) where T : class, IRepositoryItem
    {
        try
        {
            var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

            var filter = Builders<T>.Filter.Eq(o => o.Id, id);

            var deleteResult = await collection.DeleteOneAsync(filter);

            if (deleteResult is not { DeletedCount: > 0 })
            {
                throw new Exception("Delete Problem");
            }

            Console.WriteLine($"Successfully Item Deleted, Id: {id}\n");

            return true;
        }
        catch (Exception)
        {
            Console.WriteLine($"Problem Delete Item, Id : {id}\n");

            return false;
        }
    }

    public async Task<bool> DeleteManyAsync<T>(DatabaseInfo databaseInfo, IFilter filter) where T : class, IRepositoryItem
    {
        try
        {
            var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

            var composer = new MongoDbComposerFacade<T>();

            var filterDefinition = composer.Compose(filter);

            var deleteResult = await collection.DeleteManyAsync(filterDefinition);

            if (deleteResult is not { DeletedCount: > 0 })
            {
                throw new Exception("Delete Problem");
            }

            Console.WriteLine($"Successfully Delete Items, count : {deleteResult.DeletedCount}\n");

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem Delete Items by filter \n{ex.Message}\n");

            return false;
        }
    }

    public async Task<T?> GetByIdAsync<T>(DatabaseInfo databaseInfo, string id) where T : class, IRepositoryItem
    {
        var idFilter = new FilterBuilder<T>().Eq(entity => entity.Id, id);

        return await GetOneAsync<T>(databaseInfo, idFilter);
    }

    public async Task<List<T>> GetManyAsync<T>(DatabaseInfo databaseInfo) where T : class, IRepositoryItem
    {
        return await GetManyAsync<T>(databaseInfo, new FilterBuilder<T>().None());
    }

    public async Task<T?> GetOneAsync<T>(DatabaseInfo databaseInfo, IFilter filter) where T : class, IRepositoryItem
    {
        try
        {
            var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

            var composer = new MongoDbComposerFacade<T>();

            var filterDefinition = composer.Compose(filter);

            var items = await collection.FindAsync<T>(filterDefinition);

            var item = await items.FirstAsync();

            Console.WriteLine($"Successfully Get Item by filter : {item.Serialize()}\n");

            return item;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem Get Item by filter \n{ex.Message}\n");

            return default;
        }
    }

    public async Task<List<T>> GetManyAsync<T>(DatabaseInfo databaseInfo, IFilter filter) where T : class, IRepositoryItem
    {
        try
        {
            var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

            var composer = new MongoDbComposerFacade<T>();

            var filterDefinition = composer.Compose(filter);

            var itemsCursor = await collection.FindAsync<T>(filterDefinition);

            var items = await itemsCursor.ToListAsync();

            Console.WriteLine($"Successfully Get Items by filter count : {items.Count}\n");

            return items;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem Get Items by filter\n{ex.Message}\n");

            return new List<T>();
        }
    }


    public async Task<List<T>> GetManyAsync<T>(DatabaseInfo databaseInfo, IFilter filter, ISort sort, int offset, int limit) where T : class, IRepositoryItem
    {
        try
        {
            var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

            var composer = new MongoDbComposerFacade<T>();

            var filterDefinition = composer.Compose(filter);

            var sortDefinition = composer.Compose(sort);

            var itemsCursor = await collection
                .Find(filterDefinition)
                .Sort(sortDefinition)
                .Skip(offset)
                .Limit(limit)
                .ToCursorAsync();

            var items = await itemsCursor.ToListAsync();

            Console.WriteLine($"Successfully Get Items by filter count : {items.Count}\n");

            return items;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem Get Items by filter\n{ex.Message}\n");

            return new List<T>();
        }
    }

    public async Task<bool> UpdateOneAsync<T>(DatabaseInfo databaseInfo, IFilter filter, IUpdate update) where T : class, IRepositoryItem
    {
        var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

        var composer = new MongoDbComposerFacade<T>();

        var filterDefinition = composer.Compose(filter);

        var updateDefinition = composer.Compose(update);

        var result = await collection.UpdateOneAsync(filterDefinition, updateDefinition);

        return result.IsModifiedCountAvailable;
    }

    public async Task<bool> UpdateManyAsync<T>(DatabaseInfo databaseInfo, IFilter filter, IUpdate update) where T : class, IRepositoryItem
    {
        var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

        var composer = new MongoDbComposerFacade<T>();

        var filterDefinition = composer.Compose(filter);

        var updateDefinition = composer.Compose(update);

        var result = await collection.UpdateManyAsync(filterDefinition, updateDefinition);

        return result.IsModifiedCountAvailable;
    }

    public async Task<long> CountAsync<T>(DatabaseInfo databaseInfo) where T : class, IRepositoryItem
    {
        return await CountAsync<T>(databaseInfo, new FilterBuilder<T>().None());
    }

    public async Task<long> CountAsync<T>(DatabaseInfo databaseInfo, IFilter filter) where T : class, IRepositoryItem
    {
        try
        {
            var collection = _mongoClientManager.GetCollection<T>(databaseInfo);

            var composer = new MongoDbComposerFacade<T>();

            var filterDefinition = composer.Compose(filter);

            return await collection.CountDocumentsAsync(filterDefinition);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return 0;
    }
}
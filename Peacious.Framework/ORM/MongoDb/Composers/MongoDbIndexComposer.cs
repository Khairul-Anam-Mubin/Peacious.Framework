using MongoDB.Bson;
using MongoDB.Driver;
using Peacious.Framework.Extensions;
using Peacious.Framework.ORM.Interfaces;

namespace Peacious.Framework.ORM.MongoDb.Composers;

public class MongoDbIndexComposer<T> : IIndexComposer<CreateIndexModel<T>>
{
    public CreateIndexModel<T> Compose(IIndex index)
    {
        var indexKeysDictionary = index.IndexKeys.ToDictionary(
            key => key.FieldKey,
            value => value.SortDirection.SmartCast<int>());

        var document = new BsonDocument(indexKeysDictionary);

        var createIndexModel = new CreateIndexModel<T>(
            new BsonDocumentIndexKeysDefinition<T>(document));

        return createIndexModel;
    }
}
using MongoDB.Driver;
using Peacious.Framework.ORM.Enums;
using Peacious.Framework.ORM.Interfaces;

namespace Peacious.Framework.ORM.MongoDb.Composers;

public class MongoDbUpdateComposer<T> : IUpdateComposer<UpdateDefinition<T>>
{
    public UpdateDefinition<T> Compose(IUpdate update)
    {
        var updateDefinitions = new List<UpdateDefinition<T>>();
        foreach (var field in update.Fields)
        {
            switch (field.Operation)
            {
                case Operation.Set:
                    updateDefinitions.Add(Builders<T>.Update.Set(field.FieldKey, field.FieldValue));
                    break;
            }
        }

        return Builders<T>.Update.Combine(updateDefinitions);
    }
}
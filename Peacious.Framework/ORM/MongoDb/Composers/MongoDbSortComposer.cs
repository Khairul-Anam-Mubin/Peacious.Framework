using MongoDB.Driver;
using Peacious.Framework.ORM.Interfaces;
using SortDirection = Peacious.Framework.ORM.Enums.SortDirection;

namespace Peacious.Framework.ORM.MongoDb.Composers;

public class MongoDbSortComposer<T> : ISortComposer<SortDefinition<T>>
{
    public SortDefinition<T> Compose(ISort sort)
    {
        var sortDefinitions = new List<SortDefinition<T>>();

        foreach (var sortField in sort.SortFields)
        {
            switch (sortField.SortDirection)
            {
                case SortDirection.Ascending:
                    sortDefinitions.Add(Builders<T>.Sort.Ascending(sortField.FieldKey));
                    break;
                case SortDirection.Descending:
                    sortDefinitions.Add(Builders<T>.Sort.Descending(sortField.FieldKey));
                    break;
            }
        }

        return Builders<T>.Sort.Combine(sortDefinitions);
    }
}
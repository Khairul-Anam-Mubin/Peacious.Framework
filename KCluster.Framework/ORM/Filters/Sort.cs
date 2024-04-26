using KCluster.Framework.ORM.Enums;
using KCluster.Framework.ORM.Interfaces;

namespace KCluster.Framework.ORM.Filters;

public class Sort : ISort
{
    public List<ISortField> SortFields { get; set; }

    public Sort()
    {
        SortFields = new List<ISortField>();
    }

    public ISort Add(ISortField field)
    {
        SortFields.Add(field);
        return this;
    }

    public ISort Ascending(string fieldKey)
    {
        return Add(new SortField(fieldKey, SortDirection.Ascending));
    }

    public ISort Descending(string fieldKey)
    {
        return Add(new SortField(fieldKey, SortDirection.Descending));
    }
}
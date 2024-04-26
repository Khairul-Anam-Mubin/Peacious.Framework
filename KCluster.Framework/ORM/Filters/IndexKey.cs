using KCluster.Framework.ORM.Enums;
using KCluster.Framework.ORM.Interfaces;

namespace KCluster.Framework.ORM.Filters;

public class IndexKey : IIndexKey
{
    public string FieldKey { get; set; }
    public SortDirection SortDirection { get; set; }

    public IndexKey(string fieldKey, SortDirection sortDirection)
    {
        FieldKey = fieldKey;
        SortDirection = sortDirection;
    }
}
using KCluster.Framework.ORM.Enums;
using KCluster.Framework.ORM.Interfaces;

namespace KCluster.Framework.ORM.Filters;

public class SortField : ISortField
{
    public string FieldKey { get; set; }
    public SortDirection SortDirection { get; set; }

    public SortField(string fieldKey, SortDirection sortDirection)
    {
        FieldKey = fieldKey;
        SortDirection = sortDirection;
    }
}
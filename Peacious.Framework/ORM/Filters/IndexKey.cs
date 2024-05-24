using Peacious.Framework.ORM.Enums;
using Peacious.Framework.ORM.Interfaces;

namespace Peacious.Framework.ORM.Filters;

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
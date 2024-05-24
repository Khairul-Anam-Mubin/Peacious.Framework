using Peacious.Framework.ORM.Enums;
using Peacious.Framework.ORM.Interfaces;

namespace Peacious.Framework.ORM.Filters;

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
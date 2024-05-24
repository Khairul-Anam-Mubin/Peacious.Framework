using Peacious.Framework.ORM.Enums;

namespace Peacious.Framework.ORM.Interfaces;

public interface ISortField
{
    string FieldKey { get; set; }
    SortDirection SortDirection { get; set; }
}
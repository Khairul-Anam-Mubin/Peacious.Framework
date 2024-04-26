using KCluster.Framework.ORM.Enums;

namespace KCluster.Framework.ORM.Interfaces;

public interface ISortField
{
    string FieldKey { get; set; }
    SortDirection SortDirection { get; set; }
}
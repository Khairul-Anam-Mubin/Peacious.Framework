using KCluster.Framework.ORM.Enums;

namespace KCluster.Framework.ORM.Interfaces;

public interface IIndexKey
{
    string FieldKey { get; set; }
    SortDirection SortDirection { get; set; }
}
using KCluster.Framework.ORM.Enums;

namespace KCluster.Framework.ORM.Interfaces;

public interface IUpdateField
{
    string FieldKey { get; set; }
    Operation Operation { get; set; }
    object? FieldValue { get; set; }
}
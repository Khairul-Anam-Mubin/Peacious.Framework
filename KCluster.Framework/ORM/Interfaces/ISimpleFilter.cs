using KCluster.Framework.ORM.Enums;

namespace KCluster.Framework.ORM.Interfaces;

public interface ISimpleFilter
{
    string FieldKey { get; set; }
    Operator Operator { get; set; }
    object FieldValue { get; set; }
}
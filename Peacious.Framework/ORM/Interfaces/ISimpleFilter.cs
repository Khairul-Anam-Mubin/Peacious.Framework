using Peacious.Framework.ORM.Enums;

namespace Peacious.Framework.ORM.Interfaces;

public interface ISimpleFilter
{
    string FieldKey { get; set; }
    Operator Operator { get; set; }
    object FieldValue { get; set; }
}
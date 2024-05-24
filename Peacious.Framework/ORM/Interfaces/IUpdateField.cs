using Peacious.Framework.ORM.Enums;

namespace Peacious.Framework.ORM.Interfaces;

public interface IUpdateField
{
    string FieldKey { get; set; }
    Operation Operation { get; set; }
    object? FieldValue { get; set; }
}
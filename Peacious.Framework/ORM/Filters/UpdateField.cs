using Peacious.Framework.ORM.Enums;
using Peacious.Framework.ORM.Interfaces;

namespace Peacious.Framework.ORM.Filters;

internal class UpdateField : IUpdateField
{
    public string FieldKey { get; set; }
    public Operation Operation { get; set; }
    public object? FieldValue { get; set; }

    public UpdateField(string fieldKey, Operation operation, object? fieldValue = null)
    {
        FieldValue = fieldValue;
        FieldKey = fieldKey;
        Operation = operation;
    }
}
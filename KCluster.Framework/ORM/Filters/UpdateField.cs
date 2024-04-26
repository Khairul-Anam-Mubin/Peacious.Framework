using KCluster.Framework.ORM.Enums;
using KCluster.Framework.ORM.Interfaces;

namespace KCluster.Framework.ORM.Filters;

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
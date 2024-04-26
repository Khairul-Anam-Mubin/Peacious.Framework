using KCluster.Framework.ORM.Enums;
using KCluster.Framework.ORM.Interfaces;

namespace KCluster.Framework.ORM.Filters;

public class SimpleFilter : ISimpleFilter
{
    public string FieldKey { get; set; }
    public Operator Operator { get; set; }
    public object FieldValue { get; set; }

    public SimpleFilter(string fieldKey, Operator @operator, object fieldValue)
    {
        FieldKey = fieldKey;
        Operator = @operator;
        FieldValue = fieldValue;
    }
}

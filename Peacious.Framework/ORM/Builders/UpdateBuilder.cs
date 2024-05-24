using System.Linq.Expressions;
using Peacious.Framework.ORM.Enums;
using Peacious.Framework.ORM.Filters;
using Peacious.Framework.ORM.Interfaces;

namespace Peacious.Framework.ORM.Builders;

public class UpdateBuilder<TEntity>
{
    private readonly IUpdate _update;

    public UpdateBuilder()
    {
        _update = new Update();
    }

    public UpdateBuilder<TEntity> Set(string fieldKey, object? value)
    {
        _update.Add(new UpdateField(fieldKey, Operation.Set, value));
        return this;
    }

    public UpdateBuilder<TEntity> Set<TField>(Expression<Func<TEntity, TField>> field, TField value)
    {
        return Set(ExpressionHelper.GetFieldKey(field), value);
    }

    public IUpdate Build()
    {
        return _update;
    }
}
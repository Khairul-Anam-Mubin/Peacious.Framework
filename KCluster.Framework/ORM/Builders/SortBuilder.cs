using System.Linq.Expressions;
using KCluster.Framework.ORM.Enums;
using KCluster.Framework.ORM.Filters;
using KCluster.Framework.ORM.Interfaces;

namespace KCluster.Framework.ORM.Builders;

public class SortBuilder<TEntity>
{
    private readonly ISort _sort;

    public SortBuilder()
    {
        _sort = new Sort();
    }

    public SortBuilder<TEntity> Ascending(string fieldKey)
    {
        _sort.Add(new SortField(fieldKey, SortDirection.Ascending));
        return this;
    }

    public SortBuilder<TEntity> Ascending<TField>(Expression<Func<TEntity, TField>> field)
    {
        return Ascending(ExpressionHelper.GetFieldKey(field));
    }

    public SortBuilder<TEntity> Descending(string fieldKey)
    {
        _sort.Add(new SortField(fieldKey, SortDirection.Descending));
        return this;
    }

    public SortBuilder<TEntity> Descending<TField>(Expression<Func<TEntity, TField>> field)
    {
        return Descending(ExpressionHelper.GetFieldKey(field));
    }

    public ISort Build()
    {
        return _sort;
    }
}
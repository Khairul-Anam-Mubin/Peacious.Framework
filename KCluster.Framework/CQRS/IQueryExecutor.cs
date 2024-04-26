using KCluster.Framework.Results;

namespace KCluster.Framework.CQRS;

public interface IQueryExecutor
{
    Task<IResult<TResponse>> ExecuteAsync<TQuery, TResponse>(TQuery query)
        where TQuery : class, IQuery
        where TResponse : class;
}
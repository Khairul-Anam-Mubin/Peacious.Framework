using KCluster.Framework.Mediators;
using KCluster.Framework.Results;

namespace KCluster.Framework.CQRS;

public interface IQuery { }

public interface IQueryHandler<in TQuery, TResponse> : IHandler<TQuery, IResult<TResponse>>
    where TQuery : class, IQuery
    where TResponse : class
{ }
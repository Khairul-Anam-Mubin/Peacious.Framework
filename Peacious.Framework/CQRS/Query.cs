using Peacious.Framework.Mediators;
using Peacious.Framework.Results;

namespace Peacious.Framework.CQRS;

public interface IQuery<TResponse> : IRequest<IResult<TResponse>> 
    where TResponse : class { }

public interface IQueryHandler<in TQuery, TResponse> : IHandler<TQuery, IResult<TResponse>>
    where TQuery : class, IQuery<TResponse>
    where TResponse : class { }
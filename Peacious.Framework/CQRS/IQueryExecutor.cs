﻿using Peacious.Framework.Results;

namespace Peacious.Framework.CQRS;

public interface IQueryExecutor
{
    Task<IResult<TResponse>> ExecuteAsync<TQuery, TResponse>(TQuery query)
        where TQuery : class, IQuery<TResponse>
        where TResponse : class;

    Task<IResult<TResponse>> ExecuteAsync<TResponse>(IQuery<TResponse> query)
        where TResponse : class;
}
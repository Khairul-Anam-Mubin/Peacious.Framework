﻿using Peacious.Framework.IdentityScope;
using Peacious.Framework.MessageBrokers;
using Peacious.Framework.Results;

namespace Peacious.Framework.CQRS;

public interface IQueryService : IQueryExecutor
{
    Task<TResponse> GetResponseAsync<TQuery, TResponse>(TQuery query)
    where TQuery : class, IInternalMessage
    where TResponse : class;
}

public class QueryService : IQueryService
{
    private readonly IQueryExecutor _queryExecutor;
    private readonly IMessageRequestClient _messageRequestClient;
    private readonly IIdentityScopeContext _identityScopeContext;

    public QueryService(IQueryExecutor queryExecutor, IMessageRequestClient messageRequestClient, IIdentityScopeContext identityScopeContext)
    {
        _queryExecutor = queryExecutor;
        _messageRequestClient = messageRequestClient;
        _identityScopeContext = identityScopeContext;
    }

    public async Task<IResult<TResponse>> ExecuteAsync<TQuery, TResponse>(TQuery query)
        where TQuery : class, IQuery<TResponse>
        where TResponse : class
    {
        return await _queryExecutor.ExecuteAsync<TQuery, TResponse>(query);
    }

    public async Task<IResult<TResponse>> ExecuteAsync<TResponse>(IQuery<TResponse> query) where TResponse : class
    {
        return await _queryExecutor.ExecuteAsync(query);
    }

    public async Task<TResponse> GetResponseAsync<TQuery, TResponse>(TQuery query)
        where TQuery : class, IInternalMessage
        where TResponse : class
    {
        query.Token = _identityScopeContext.Token;
        return await _messageRequestClient.GetResponseAsync<TQuery, TResponse>(query);
    }
}

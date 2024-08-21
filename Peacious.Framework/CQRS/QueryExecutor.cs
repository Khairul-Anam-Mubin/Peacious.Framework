using Peacious.Framework.Extensions;
using Peacious.Framework.Mediators;
using Peacious.Framework.Results;
using Peacious.Framework.Results.Errors;

namespace Peacious.Framework.CQRS;

public class QueryExecutor : IQueryExecutor
{
    private readonly IMediator _mediator;

    public QueryExecutor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IResult<TResponse>> ExecuteAsync<TQuery, TResponse>(TQuery query)
        where TQuery : class, IQuery<TResponse>
        where TResponse : class
    {
        var validationResult = query.GetValidationResult<TResponse>();

        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        try
        {
            var response = await _mediator.SendAsync(query);

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            return Error.Failure("Execution failed", e.Message).Result<TResponse>();
        }
    }

    public async Task<IResult<TResponse>> ExecuteAsync<TResponse>(IQuery<TResponse> query) where TResponse : class
    {
        var validationResult = query.GetValidationResult<TResponse>();

        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        try
        {
            var response = await _mediator.SendAsync(query);

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            return Error.Failure("Execution failed", e.Message).Result<TResponse>();
        }
    }
}
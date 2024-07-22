using Peacious.Framework.Extensions;
using Peacious.Framework.Mediators;
using Peacious.Framework.Results;
using Peacious.Framework.Results.Errors;

namespace Peacious.Framework.CQRS;

public class CommandExecutor : ICommandExecutor
{
    private readonly IMediator _mediator;

    public CommandExecutor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IResult> ExecuteAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand
    {
        var validationResult = command.GetValidationResult();

        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        try
        {
            var result = await _mediator.SendAsync(command);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return Error.Failure("Execution failed", e.Message).Result();
        }
    }

    public async Task<IResult<TResponse>> ExecuteAsync<TResponse>(ICommand<TResponse> command)
        where TResponse : class
    {
        var validationResult = command.GetValidationResult<TResponse>();

        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        try
        {
            var result = await _mediator.SendAsync(command);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return Error.Failure("Execution failed", e.Message).Result<TResponse>();
        }
    }
}
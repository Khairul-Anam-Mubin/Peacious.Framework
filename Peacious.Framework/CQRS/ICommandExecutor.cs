using Peacious.Framework.Results;

namespace Peacious.Framework.CQRS;

public interface ICommandExecutor
{
    Task<IResult> ExecuteAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand;

    Task<IResult<TResponse>> ExecuteAsync<TResponse>(ICommand<TResponse> command)
        where TResponse : class;
}
using KCluster.Framework.Results;

namespace KCluster.Framework.CQRS;

public interface ICommandExecutor
{
    Task<IResult> ExecuteAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand;

    Task<IResult<TResponse>> ExecuteAsync<TCommand, TResponse>(TCommand command)
        where TCommand : class, ICommand
        where TResponse : class;
}
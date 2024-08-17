using Peacious.Framework.IdentityScope;
using Peacious.Framework.MessageBrokers;
using Peacious.Framework.Results;

namespace Peacious.Framework.CQRS;

public interface ICommandService : ICommandExecutor
{
    /// <summary>
    /// Sends the command to message queue
    /// </summary>
    Task SendAsync<TCommand>(TCommand command) where TCommand : class, IInternalMessage;
}

public class CommandService : ICommandService
{
    private readonly ICommandExecutor _commandExecutor;
    private readonly ICommandBus _commandBus;
    private readonly IIdentityScopeContext _identityScopeContext;

    public CommandService(ICommandExecutor commandExecutor, ICommandBus commandBus, IIdentityScopeContext identityScopeContext)
    {
        _commandExecutor = commandExecutor;
        _commandBus = commandBus;
        _identityScopeContext = identityScopeContext;
    }

    public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, IInternalMessage
    {
        command.Token = _identityScopeContext.Token;
        await _commandBus.SendAsync(command);
    }

    public async Task<IResult> ExecuteAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        return await _commandExecutor.ExecuteAsync(command);
    }

    public async Task<IResult<TResponse>> ExecuteAsync<TResponse>(ICommand<TResponse> command)
        where TResponse : class
    {
        return await _commandExecutor.ExecuteAsync(command);
    }
}

using Peacious.Framework.Mediators;
using Peacious.Framework.Results;

namespace Peacious.Framework.CQRS;

public interface ICommand { }

public interface ICommandHandler<in TCommand> : IHandler<TCommand, IResult>
    where TCommand : class, ICommand
{ }

public interface ICommandHandler<in TCommand, TResponse> : IHandler<TCommand, IResult<TResponse>>
    where TCommand : class, ICommand
    where TResponse : class
{ }
using Peacious.Framework.IdentityScope;
using Peacious.Framework.MessageBrokers;
using Peacious.Framework.Results;

namespace Peacious.Framework.CQRS;

public abstract class ACommandConsumer<TCommand, TResponse> :
    AMessageConsumer<TCommand>,
    ICommandHandler<TCommand, TResponse>
    where TCommand : class, ICommand<TResponse>, IInternalMessage
    where TResponse : class
{
    protected readonly IIdentityScopeContext IdentityScopeContext;

    protected ACommandConsumer(IIdentityScopeContext identityScopeContext)
    {
        IdentityScopeContext = identityScopeContext;
    }

    protected abstract Task<IResult<TResponse>> OnConsumeAsync(TCommand command, IMessageContext<TCommand>? context = null);

    public override async Task Consume(IMessageContext<TCommand> context)
    {
        IdentityScopeContext.Initiate(context.Message.Token);

        await OnConsumeAsync(context.Message, context);
    }

    public async Task<IResult<TResponse>> HandleAsync(TCommand request)
    {
        return await OnConsumeAsync(request);
    }

    public async Task<IResult<TResponse>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await OnConsumeAsync(request);
    }
}

public abstract class ACommandConsumer<TCommand> :
    AMessageConsumer<TCommand>,
    ICommandHandler<TCommand>
    where TCommand : class, ICommand, IInternalMessage
{
    protected readonly IIdentityScopeContext IdentityScopeContext;

    protected ACommandConsumer(IIdentityScopeContext identityScopeContext)
    {
        IdentityScopeContext = identityScopeContext;
    }

    protected abstract Task<IResult> OnConsumeAsync(TCommand command, IMessageContext<TCommand>? context = null);

    public override async Task Consume(IMessageContext<TCommand> context)
    {
        IdentityScopeContext.Initiate(context.Message.Token);

        await OnConsumeAsync(context.Message, context);
    }

    public async Task<IResult> HandleAsync(TCommand request)
    {
        return await OnConsumeAsync(request);
    }

    public async Task<IResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await OnConsumeAsync(request);
    }
}
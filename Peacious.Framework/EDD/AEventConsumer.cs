using Peacious.Framework.CQRS;
using Peacious.Framework.IdentityScope;
using Peacious.Framework.MessageBrokers;

namespace Peacious.Framework.EDD;

public abstract class AEventConsumer<TEvent> : AMessageConsumer<TEvent>, IEventHandler<TEvent>
    where TEvent : class, IEvent, IInternalMessage
{
    protected readonly IIdentityScopeContext IdentityScopeContext;

    protected abstract Task OnConsumeAsync(TEvent @event, IMessageContext<TEvent>? context = null);

    protected AEventConsumer(IIdentityScopeContext identityScopeContext)
    {
        IdentityScopeContext = identityScopeContext;
    }

    public override async Task Consume(IMessageContext<TEvent> context)
    {
        IdentityScopeContext.Initiate(context.Message.Token);

        await OnConsumeAsync(context.Message, context);
    }

    public async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        await OnConsumeAsync(notification);
    }
}

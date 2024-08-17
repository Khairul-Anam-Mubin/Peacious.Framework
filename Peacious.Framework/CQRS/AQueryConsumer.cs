using Peacious.Framework.IdentityScope;
using Peacious.Framework.MessageBrokers;
using Peacious.Framework.Results;

namespace Peacious.Framework.CQRS;

public abstract class AQueryConsumer<TQuery, TResponse> :
    AMessageConsumer<TQuery>,
    IQueryHandler<TQuery, TResponse>
    where TQuery : class, IQuery<TResponse>, IInternalMessage
    where TResponse : class
{
    protected readonly IIdentityScopeContext IdentityScopeContext;

    protected AQueryConsumer(IIdentityScopeContext identityScopeContext)
    {
        IdentityScopeContext = identityScopeContext;
    }

    protected abstract Task<IResult<TResponse>> OnConsumeAsync(TQuery query, IMessageContext<TQuery>? context = null);

    public override async Task Consume(IMessageContext<TQuery> context)
    {
        IdentityScopeContext.Initiate(context.Message.Token);

        var response = await OnConsumeAsync(context.Message, context);

        await context.RespondAsync(response.Value!);
    }

    public async Task<IResult<TResponse>> HandleAsync(TQuery request)
    {
        return await OnConsumeAsync(request);
    }

    public async Task<IResult<TResponse>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        return await OnConsumeAsync(request);
    }
}
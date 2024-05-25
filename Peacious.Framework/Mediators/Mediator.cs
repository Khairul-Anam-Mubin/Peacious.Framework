namespace Peacious.Framework.Mediators;

public class Mediator : IMediator
{
    private readonly MediatR.IMediator _mediator;

    public Mediator(MediatR.IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) 
        where TResponse : class
    {
        return await _mediator.Send(request);
    }

    public async Task SendAsync<TRequest>(TRequest request)
        where TRequest : class, IRequest
    {
        await _mediator.Send(request);
    }

    public async Task PublishAsync<TNotification>(TNotification notification) 
        where TNotification: class, INotification
    {
        await _mediator.Publish(notification);
    }
}
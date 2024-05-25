namespace Peacious.Framework.Mediators;

public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) 
        where TResponse : class;

    Task SendAsync<TRequest>(TRequest request) 
        where TRequest : class, IRequest;

    Task PublishAsync<TNotification>(TNotification notification) 
        where TNotification : class, INotification;
}
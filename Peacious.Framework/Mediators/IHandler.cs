namespace Peacious.Framework.Mediators;

public interface IRequest : MediatR.IRequest { }

public interface IRequest<TResponse> : MediatR.IRequest<TResponse> 
    where TResponse : class {}

public interface INotification : MediatR.INotification { }

public interface IHandler<in TRequest> : MediatR.IRequestHandler<TRequest> 
    where TRequest : class, IRequest {}

public interface IHandler<in TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse> 
    where TRequest : class, IRequest<TResponse>
    where TResponse : class {}

public interface INotificationHandler<TNotificaiton> : MediatR.INotificationHandler<TNotificaiton> 
    where TNotificaiton : class, INotification
{ }
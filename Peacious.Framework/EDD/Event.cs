using MediatR;

namespace Peacious.Framework.EDD;

public interface IEvent : INotification { }

public interface IEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : class, IEvent
{ }

using System.Collections.Generic;
using CQRSTutorial.Core;

namespace CQRSTutorial.Messaging
{
    public class InMemoryMessageBus : IMessageBus
    {
        IList<IEventHandler> _handlers = new List<IEventHandler>();

        public void RegisterEventHandler<T>(T eventHandler) where T : IEventHandler
        {
            _handlers.Add(eventHandler);
        }

        public void RaiseEvent<T>(IDomainEvent domainEvent) where T : IDomainEvent
        {
            var @event = (T)domainEvent;

            foreach (var eventHandler in _handlers)
            {
                eventHandler.Handle(@event);
            }
        }
    }

    public interface IMessageBus
    {
        void RegisterEventHandler<T>(T eventHandler) where T : IEventHandler;

        void RaiseEvent<T>(IDomainEvent domainEvent) where T : IDomainEvent;
    }
}

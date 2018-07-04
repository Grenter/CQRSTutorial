using System.Collections.Generic;
using CQRSTutorial.Core;

namespace CQRSTutorial.Messaging
{
    public class InMemoryMessageBus : IMessageBus
    {
        readonly IList<IEventListener> _handlers = new List<IEventListener>();

        public void RegisterEventHandler<T>(T eventHandler) where T : IEventListener
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
}

using CQRSTutorial.Core;
using CQRSTutorial.Messaging;

namespace CQRSTutorial.EventStore
{
    public class EventListener : IEventListener
    {
        private readonly IMessageBus _messageBus;
        private readonly IEventRepository _eventRepository;

        public EventListener(IMessageBus messageBus, IEventRepository eventRepository)
        {
            _messageBus = messageBus;
            _eventRepository = eventRepository;
            
            _messageBus.RegisterEventHandler(this);
        }

        public void Handle(IDomainEvent domainEvents)
        {
            _eventRepository.Add(domainEvents);
        }
    }
}

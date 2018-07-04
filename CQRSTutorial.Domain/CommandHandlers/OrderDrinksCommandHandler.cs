using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Core.Exceptions;
using CQRSTutorial.EventStore;
using System.Linq;
using CQRSTutorial.Messaging;

namespace CQRSTutorial.Domain.CommandHandlers
{
    public class OrderDrinksCommandHandler : ICommandHandler<OrderDrinks>
    {
        private readonly IEventRepository _repository;
        private readonly IMessageBus _messageBus;

        public OrderDrinksCommandHandler(IEventRepository eventRepository, IMessageBus messageBus)
        {
            _repository = eventRepository;
            _messageBus = messageBus;
        }

        public IDomainEvent Handle(OrderDrinks command)
        {
            var events = _repository.GetEventsFor(command.AggregateId);
            var tab = TabAggregate.BuildFromHistory(events.ToList());

            if (tab is null) throw new NullAggregateException();

            tab.AddDrinks(command);

            var raisedEvent = tab.GetDomainEvents().Last();

            _messageBus.RaiseEvent(raisedEvent);

            return raisedEvent;
        }
    }
}
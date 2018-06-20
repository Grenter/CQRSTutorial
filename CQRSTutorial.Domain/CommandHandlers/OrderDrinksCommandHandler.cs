using System;
using System.Linq;
using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Events;
using CQRSTutorial.EventStore;

namespace CQRSTutorial.Domain.CommandHandlers
{
    public class OrderDrinksCommandHandler : ICommandHandler<OrderDrinks>
    {
        // Notes for discussion:
        // I changed handle from void to return the event.
        // When the handler uncovers an error based on logic (tab not open) should it raise an error event? rather than an exception
        // 

        private readonly IEventRepository _repository;

        public OrderDrinksCommandHandler(IEventRepository eventRepository)
        {
            _repository = eventRepository;
        }

        public IDomainEvent Handle(OrderDrinks command)
        {
            var events = _repository.GetEventsFor(command.AggregateId);
            var tab = TabAggregate.BuildFromHistory(events);

            tab.Apply(new DrinksOrdered
            {
                Id = Guid.NewGuid(),
                AggregateId = command.AggregateId,
                OrderItems = command.OrderItems
            });

            var raisedEvent = tab.GetDomainEvents().Last();

            _repository.Add(raisedEvent); // Temp until Event Listeners and bus added. 

            return raisedEvent;
        }
    }
}
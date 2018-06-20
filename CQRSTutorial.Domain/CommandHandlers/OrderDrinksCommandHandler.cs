using System.Linq;
using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Events;
using CQRSTutorial.EventStore;

namespace CQRSTutorial.Domain.CommandHandlers
{
    public class OrderDrinksCommandHandler : ICommandHandler<OrderDrinks>
    {
        private readonly IEventRepository _repository;

        public OrderDrinksCommandHandler(IEventRepository eventRepository)
        {
            _repository = eventRepository;
        }

        public IDomainEvent Handle(OrderDrinks command)
        {
            var events = _repository.GetEventsFor(command.AggregateId);
            var tab = new TabAggregate();
            foreach (var domainEvent in events)
            {
                tab.Apply(domainEvent);
            }

            tab.Apply(new DrinksOrdered
            {
                AggregateId = command.AggregateId,
                OrderItems = command.OrderItems
            });

            var raisedEvent = tab.GetDomainEvents().Last();

            _repository.Add(raisedEvent); // Temp until Event Listeners and bus added. 

            return raisedEvent;
        }
    }
}
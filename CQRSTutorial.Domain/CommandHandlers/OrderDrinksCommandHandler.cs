using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Core.Exceptions;
using CQRSTutorial.EventStore;
using System.Linq;

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
            var tab = TabAggregate.BuildFromHistory(events);

            if (tab is null) throw new NullAggregateException();

            tab.AddDrinks(command);

            var raisedEvent = tab.GetDomainEvents().Last();
            _repository.Add(raisedEvent); // Temp until Event Listeners and bus added. 
            return raisedEvent;
        }
    }
}
using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Events;
using CQRSTutorial.EventStore;
using System;
using System.Linq;

namespace CQRSTutorial.Domain.CommandHandlers
{
    public class OpenTabCommandHandler : ICommandHandler<OpenTab>
    {
        private readonly IEventRepository _repository;

        public OpenTabCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public IDomainEvent Handle(OpenTab command)
        {
            var tab = new TabAggregate(command.AggregateId, command.TableNumber, command.WaiterName);
            var raisedEvent = tab.GetDomainEvents().Last();

            _repository.Add(raisedEvent); // Temp until Event Listeners and bus added. 

            return raisedEvent;
        }
    }

    public class DrinksOrderCommandHandler : ICommandHandler<DrinksOrder>
    {
        private readonly IEventRepository _repository;

        public DrinksOrderCommandHandler(IEventRepository eventRepository)
        {
            _repository = eventRepository;
        }

        public IDomainEvent Handle(DrinksOrder command)
        {
            var events = _repository.GetEventsFor(command.AggregateId);
            var tab = new TabAggregate();
            foreach (var domainEvent in events)
            {
                tab.Apply(domainEvent);
            }

            tab.Apply(new OrderedDrinks
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

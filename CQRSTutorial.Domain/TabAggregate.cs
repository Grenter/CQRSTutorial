using CQRSTutorial.Core;
using CQRSTutorial.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using CQRSTutorial.Commands;

namespace CQRSTutorial.Domain
{
    public class TabAggregate : Aggregate,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>,
        IApplyEvent<TabError>
    {
        private bool _isOpen;
        private decimal _tabBalance;
        private readonly List<OrderItem> _drinks = new List<OrderItem>();

        private TabAggregate()
        { }

        public TabAggregate(Guid tabId, int tableNumber, string waiterName)
        {
            var tabOpened = new TabOpened
            {
                //Id = Guid.NewGuid(),
                AggregateId = tabId,
                WaiterName = waiterName,
                TableNumber = tableNumber,
                Balance = 0.0m
            };

            Apply(tabOpened);
        }

        public override IEnumerable<IDomainEvent> GetDomainEvents()
        {
            return Events;
        }

        public sealed override void Apply(IDomainEvent domainEvent)
        {
            Events.Add(domainEvent);
            When((dynamic)domainEvent);
        }

        public void When(TabOpened @event)
        {
            _isOpen = true;
            _tabBalance = @event.Balance;
        }

        public void When(DrinksOrdered @event)
        {
            _tabBalance = @event.OrderItems.Sum(oi => oi.Price);
            _drinks.AddRange(@event.OrderItems);
        }

        public void When(TabError @event)
        {
        }

        public void AddDrinks(OrderDrinks command)
        {
            if (_isOpen)
            {
                Apply(new DrinksOrdered
                {
                    Id = Guid.NewGuid(),
                    AggregateId = command.AggregateId,
                    OrderItems = command.OrderItems
                });
            }
        }

        public static TabAggregate BuildFromHistory(IList<IDomainEvent> domainEvents)
        {
            if (!domainEvents.Any()) return null;

            var tab = new TabAggregate();

            foreach (var domainEvent in domainEvents)
            {
                tab.Apply(domainEvent);
            }

            return tab;
        }

        public void LastEvent(Action<IDomainEvent> action)
        {
            
        }
    }
}

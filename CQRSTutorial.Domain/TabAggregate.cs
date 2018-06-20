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

        private TabAggregate()
        { }

        public TabAggregate(Guid tabId, int tableNumber, string waiterName)
        {
            var tabOpened = new TabOpened
            {
                Id = Guid.NewGuid(),
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

        public override void Apply(IDomainEvent domainEvent)
        {
            Events.Add(domainEvent);
            When((dynamic)domainEvent);
        }

        public void When(TabOpened domainEvent)
        {
            _isOpen = true;
            _tabBalance = domainEvent.Balance;
        }

        public void When(DrinksOrdered domainEvent)
        {
            _tabBalance = domainEvent.OrderItems.Sum(oi => oi.Price);
        }

        public void When(TabError domainEvent)
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

        public static TabAggregate BuildFromHistory(IEnumerable<IDomainEvent> domainEvents)
        {
            if (!domainEvents.Any()) return null;

            var tab = new TabAggregate();

            foreach (var domainEvent in domainEvents)
            {
                tab.Apply(domainEvent);
            }

            return tab;

        }
    }
}

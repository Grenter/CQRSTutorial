using System;
using System.Collections.Generic;
using CQRSTutorial.Events;

namespace CQRSTutorial.Core
{
    public class TabAggregate : Aggregate,
        IApplyEvent<TabOpened>
    {
        private bool _isOpen;
        private decimal _tabBalance;

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

            Events.Add(tabOpened);
            Apply(tabOpened);
        }

        public TabAggregate(TabOpened tabOpened)
        {
            Events.Add(tabOpened);
            Apply(tabOpened);
        }

        public IEnumerable<IDomainEvent> GetDomainEvents()
        {
            return Events;
        }

        public void Apply(TabOpened domainEvent)
        {
            _isOpen = true;
            _tabBalance = domainEvent.Balance;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using CQRSTutorial.Core.Events;

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

            Events.Add(domainEvent);
        }
    }
}

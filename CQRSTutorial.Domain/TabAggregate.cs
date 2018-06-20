﻿using CQRSTutorial.Core;
using CQRSTutorial.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRSTutorial.Domain
{
    public class TabAggregate : Aggregate,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>
    {
        private bool _isOpen;
        private decimal _tabBalance;

        public TabAggregate()
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
    }
}
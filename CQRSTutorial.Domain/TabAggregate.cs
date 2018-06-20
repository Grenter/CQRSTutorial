﻿using CQRSTutorial.Core;
using CQRSTutorial.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRSTutorial.Domain
{
    public class TabAggregate : Aggregate,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>,
        IApplyEvent<TabError>
    {
        public bool IsOpen { get; private set; }
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
            IsOpen = true;
            _tabBalance = domainEvent.Balance;
        }

        public void When(DrinksOrdered domainEvent)
        {
            _tabBalance = domainEvent.OrderItems.Sum(oi => oi.Price);
        }

        public void When(TabError domainEvent)
        {
        }

        public static TabAggregate BuildFromHistory(IEnumerable<IDomainEvent> domainEvents)
        {
            var tab = new TabAggregate();

            foreach (var domainEvent in domainEvents)
            {
                tab.Apply(domainEvent);
            }

            return tab;
        }
    }
}

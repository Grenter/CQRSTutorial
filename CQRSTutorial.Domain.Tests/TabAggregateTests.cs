using System;
using System.Collections.Generic;
using System.Linq;
using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Events;
using FluentAssertions;
using NUnit.Framework;

namespace CQRSTutorial.Domain.Tests
{
    [TestFixture]
    public class TabAggregateTests
    {
        private Guid _tabId;

        [SetUp]
        public void SetUp()
        {
            _tabId = new Guid("F747B574-5BB2-46E1-BEF1-A86AEFEA8ECE");
        }

        [Test]
        public void When_new_tab_aggregate_tab_opened_event_raised()
        {
            var tab = new TabAggregate(_tabId,45,"Gary");

            var tabEvent = tab.GetDomainEvents().Last() as TabOpened;

            tabEvent.AggregateId.Should().Be(_tabId);
            tabEvent.TableNumber.Should().Be(45);
            tabEvent.WaiterName.Should().Be("Gary");
            tabEvent.Balance.Should().Be(0.0m);
        }

        [Test]
        public void When_drinks_added()
        {
            var tab = TabAggregate.BuildFromHistory(new List<IDomainEvent>
            {
                new TabOpened
                {
                    AggregateId = _tabId,
                    WaiterName = "Gary",
                    TableNumber = 45,
                    Balance = 0.0m
                }
            });

            tab.AddDrinks(new OrderDrinks
            {
                Id = Guid.NewGuid(),
                AggregateId = _tabId,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Name = "Coke (pint)",
                        Price = 2.0m
                    }
                }
            });

            var tabEvent = tab.GetDomainEvents().Last() as DrinksOrdered;

            tabEvent.OrderItems.Count.Should().Be(1);
        }
    }
}

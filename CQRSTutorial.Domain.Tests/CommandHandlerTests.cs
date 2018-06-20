using CQRSTutorial.Commands;
using CQRSTutorial.Domain.CommandHandlers;
using CQRSTutorial.EventStore;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using CQRSTutorial.Core;
using CQRSTutorial.Core.Exceptions;
using CQRSTutorial.Events;
using NSubstitute.Core;

namespace CQRSTutorial.Domain.Tests
{
    [TestFixture]
    public class CommandHandlerTests
    {
        private readonly Guid _aggregateId = new Guid("5DCD69E3-1CF1-45A8-AEC9-217E4F8DA400");
        private IEventRepository _eventRepository;

        [SetUp]
        public void Setup()
        {
            _eventRepository = Substitute.For<IEventRepository>();
        }

        [Test]
        public void When_open_tab_command_raised()
        {
            var commandHandler = new OpenTabCommandHandler(_eventRepository);
            var openTab = new OpenTab
            {
                AggregateId = _aggregateId,
                WaiterName = "Ronald",
                TableNumber = 65
            };

            var raisedEvent = commandHandler.Handle(openTab);

            Assert.That(raisedEvent, Is.TypeOf<TabOpened>());
        }

        [Test]
        public void When_order_drinks_command_raised_after_open_tab()
        {
            _eventRepository.GetEventsFor(_aggregateId).Returns(new List<IDomainEvent>
            {
                new TabOpened
                {
                    AggregateId = _aggregateId,
                    WaiterName = "Drew",
                    TableNumber = 45
                }
            });

            var commandHandler = new OrderDrinksCommandHandler(_eventRepository);
            var drinksOrder = new OrderDrinks
            {
                AggregateId = _aggregateId,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Name = "Coke (pint)",
                        Price = 2.0m
                    }
                }
            };

            var raisedEvent = commandHandler.Handle(drinksOrder) as DrinksOrdered;

            Assert.That(raisedEvent.OrderItems.Count, Is.EqualTo(1));
        }

        [Test]
        public void When_order_drinks_command_raised_with_no_open_tab()
        {
            _eventRepository.GetEventsFor(_aggregateId).Returns(new List<IDomainEvent>());

            var commandHandler = new OrderDrinksCommandHandler(_eventRepository);
            var drinksOrder = new OrderDrinks
            {
                AggregateId = _aggregateId,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Name = "Coke (pint)",
                        Price = 2.0m
                    }
                }
            };

            Assert.Throws<NullAggregateException>(() => commandHandler.Handle(drinksOrder));
        }
    }
}

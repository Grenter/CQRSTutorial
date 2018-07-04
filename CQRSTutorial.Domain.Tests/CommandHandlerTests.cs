using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Core.Exceptions;
using CQRSTutorial.Domain.CommandHandlers;
using CQRSTutorial.Events;
using CQRSTutorial.EventStore;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using CQRSTutorial.Messaging;

namespace CQRSTutorial.Domain.Tests
{
    [TestFixture]
    public class CommandHandlerTests
    {
        private readonly Guid _aggregateId = new Guid("5DCD69E3-1CF1-45A8-AEC9-217E4F8DA400");
        private IEventRepository _eventRepository;
        private IMessageBus _messageBus;

        [SetUp]
        public void Setup()
        {
            _eventRepository = Substitute.For<IEventRepository>();
            _messageBus = Substitute.For<IMessageBus>();
        }

        [Test]
        public void When_open_tab_command_check_event_raised()
        {
            var commandHandler = new OpenTabCommandHandler(_messageBus);
            var openTab = new OpenTab
            {
                AggregateId = _aggregateId,
                WaiterName = "Ronald",
                TableNumber = 65
            };

            commandHandler.Handle(openTab);

            _messageBus.Received().RaiseEvent(Arg.Any<TabOpened>());
        }

        [Test]
        public void When_order_drinks_command_after_open_tab_command_drinks_ordered_event_raised()
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

            var commandHandler = new OrderDrinksCommandHandler(_eventRepository, _messageBus);
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

            commandHandler.Handle(drinksOrder);

            _messageBus.Received().RaiseEvent(Arg.Any<DrinksOrdered>());
        }

        [Test]
        public void When_order_drinks_command_raised_with_no_open_tab()
        {
            _eventRepository.GetEventsFor(_aggregateId).Returns(new List<IDomainEvent>());

            var commandHandler = new OrderDrinksCommandHandler(_eventRepository, _messageBus);
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

            Action handle = () => commandHandler.Handle(drinksOrder);

            handle.Should().Throw<NullAggregateException>();
        }
    }
}

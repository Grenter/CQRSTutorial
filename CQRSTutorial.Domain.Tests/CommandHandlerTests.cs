using CQRSTutorial.Commands;
using CQRSTutorial.Domain.CommandHandlers;
using CQRSTutorial.EventStore;
using NSubstitute;
using NUnit.Framework;
using System;

namespace CQRSTutorial.Domain.Tests
{
    [TestFixture]
    public class CommandHandlerTests
    {
        private readonly Guid _aggregateId = new Guid("5DCD69E3-1CF1-45A8-AEC9-217E4F8DA400");

        [Test]
        public void When_open_tab_command_raised()
        {
            var eventRepo = Substitute.For<IEventRepository>();

            var commandHandler = new OpenTabCommandHandler(eventRepo);
            var openTab = new OpenTab
            {
                AggregateId = _aggregateId,
                WaiterName = "Ronald",
                TableNumber = 65
            };

            var raisedEvent = commandHandler.Handle(openTab);

            Assert.That(raisedEvent.AggregateId, Is.EqualTo(_aggregateId));
        }

        [Test]
        public void When_order_drinks_command_raised()
        {
            
        }
    }
}

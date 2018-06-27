using System;
using System.Collections.Generic;
using CQRSTutorial.Core;
using NSubstitute;
using NUnit.Framework;

namespace CQRSTutorial.Messaging.Tests
{
    [TestFixture]
    public class InMemoryMessageBusTests
    {
        [Test]
        public void Registered_handlers_recieve_raised_events()
        {
            var bus = new InMemoryMessageBus();
            var eventHandler = Substitute.For<IEventHandler>();
            var fakeEvent = new FakeEvent();

            bus.RegisterEventHandler(eventHandler);

            bus.RaiseEvent<FakeEvent>(fakeEvent);

            eventHandler.Received().Handle(fakeEvent);
        }
    }

    public class FakeEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
    }

    public class EventHandler : IEventHandler
    {
        public void Handle(IDomainEvent domainEvents)
        {
            
        }
    }
}

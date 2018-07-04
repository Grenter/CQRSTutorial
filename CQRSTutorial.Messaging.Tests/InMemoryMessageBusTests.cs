using CQRSTutorial.Core;
using NSubstitute;
using NUnit.Framework;
using System;

namespace CQRSTutorial.Messaging.Tests
{
    [TestFixture]
    public class InMemoryMessageBusTests
    {
        [Test]
        public void Registered_handlers_recieve_raised_events()
        {
            var bus = new InMemoryMessageBus();
            var eventHandler = Substitute.For<IEventListener>();
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

    public class EventListener : IEventListener
    {
        public void Handle(IDomainEvent domainEvents)
        {
            
        }
    }
}

using System;
using CQRSTutorial.Core;
using CQRSTutorial.Messaging;
using NSubstitute;
using NUnit.Framework;

namespace CQRSTutorial.EventStore.Tests
{
    [TestFixture]
    public class EventListenerTests
    {
        [Test]
        public void Register_with_message_bus_and_can_handle_event()
        {
            var messageBus = Substitute.For<IMessageBus>();
            var eventRepository = Substitute.For<IEventRepository>();
            var eventListener = Substitute.For<EventListener>(messageBus, eventRepository);

            var fakeEvent = new FakeEvent
            {
                Id = Guid.NewGuid(),
                AggregateId = Guid.NewGuid()
            };

            messageBus.RaiseEvent<FakeEvent>(fakeEvent);
            
            eventListener.Received().Handle(fakeEvent);
            eventRepository.Received().Add(fakeEvent);
        }
    }

    public class FakeEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
    }
}

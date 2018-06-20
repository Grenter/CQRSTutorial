using CQRSTutorial.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRSTutorial.Core;

namespace CQRSTutorial.EventStore
{
    public class EventRepository : IEventRepository
    {
        private readonly EventStoreContext _context;

        public EventRepository(EventStoreContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async void Add(IDomainEvent domainEvent)
        {
            await _context.AddAsync(new Event
            {
                AggregateId = domainEvent.AggregateId,
                Type = domainEvent.GetType().FullName,
                Data = JsonConvert.SerializeObject(domainEvent)
            });

            await _context.SaveChangesAsync();
        }

        public IEnumerable<IDomainEvent> GetEventsFor(Guid aggregateId)
        {
            var events = _context.Events.Where(ev => ev.AggregateId == aggregateId);

            return events.Select(ev => DeserialiseDomainEvent(ev)).ToList();
        }

        private IDomainEvent DeserialiseDomainEvent(Event @event)
        {
            var eventAssembly = Assembly.LoadFile(@"C:\code\CQRSTutorial\CQRSTutorial.Events\bin\Debug\netstandard2.0\CQRSTutorial.Events.dll");

            var type = eventAssembly.GetType(@event.Type);

            return (IDomainEvent) JsonConvert.DeserializeObject(@event.Data, type);
        }

        public IList<Event> GetAllEvents(Guid aggregateId)
        {
            var events = _context.Events.Where(ev => ev.AggregateId == aggregateId);

            return events.ToList();
        }
    }
}
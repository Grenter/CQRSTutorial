using CQRSTutorial.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CQRSTutorial.EventStore
{
    public class EventRepository : IEventRepository
    {
        private readonly EventStoreContext _context;
        private readonly Assembly _eventsAssembly;

        public EventRepository(EventStoreContext context, Assembly eventsAssembly)
        {
            _eventsAssembly = eventsAssembly;

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
            var type = _eventsAssembly.GetType(@event.Type);

            return (IDomainEvent) JsonConvert.DeserializeObject(@event.Data, type);
        }
    }
}
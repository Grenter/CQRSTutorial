using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTutorial.Core.Events;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
                Name = domainEvent.ToString(),
                Data = JsonConvert.SerializeObject(domainEvent)
            });

            await _context.SaveChangesAsync();
        }

        public IList<Event> GetAllEvents(Guid aggregateId)
        {
            var events = _context.Events.Where(ev => ev.AggregateId == aggregateId);

            return events.ToList();
        }
    }
}
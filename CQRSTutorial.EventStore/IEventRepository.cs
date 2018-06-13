using System;
using System.Collections.Generic;
using CQRSTutorial.Core.Events;

namespace CQRSTutorial.EventStore
{
    public interface IEventRepository
    {
        void Add(IDomainEvent domainEvent);

        IList<Event> GetAllEvents(Guid aggregateId);
    }
}
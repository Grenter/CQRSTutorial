using CQRSTutorial.Core;
using System;
using System.Collections.Generic;

namespace CQRSTutorial.EventStore
{
    public interface IEventRepository
    {
        void Add(IDomainEvent domainEvent);

        IEnumerable<IDomainEvent> GetEventsFor(Guid aggregateId);
    }
}
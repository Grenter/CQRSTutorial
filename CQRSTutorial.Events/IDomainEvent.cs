using System;

namespace CQRSTutorial.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; set; }
        Guid AggregateId { get; set; }
    }
}
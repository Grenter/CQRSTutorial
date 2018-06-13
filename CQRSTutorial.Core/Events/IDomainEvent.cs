using System;

namespace CQRSTutorial.Core.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; set; }
        Guid AggregateId { get; set; }
    }
}
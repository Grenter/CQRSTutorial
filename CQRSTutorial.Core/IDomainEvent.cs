using System;

namespace CQRSTutorial.Core
{
    public interface IDomainEvent
    {
        Guid Id { get; set; }
        Guid AggregateId { get; set; }
    }
}
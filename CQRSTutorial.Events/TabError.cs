using System;
using System.Collections.Generic;
using CQRSTutorial.Core;

namespace CQRSTutorial.Events
{
    public class TabError : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string Reason { get; set; }
    }
}
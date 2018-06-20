using System;
using System.Collections.Generic;
using CQRSTutorial.Core;

namespace CQRSTutorial.Events
{
    public class DrinksOrdered : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
using System;
using System.Collections.Generic;
using CQRSTutorial.Core;

namespace CQRSTutorial.Events
{
    public class TabOpened : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string WaiterName { get; set; }
        public int TableNumber { get; set; }
        public decimal Balance { get; set; }
    }

    public class OrderedDrinks : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
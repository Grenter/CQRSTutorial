using System;

namespace CQRSTutorial.Core.Events
{
    public class TabOpened : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string WaiterName { get; set; }
        public int TableNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
using CQRSTutorial.Core;
using System;
using System.Collections.Generic;

namespace CQRSTutorial.Commands
{
    public class OpenTab : ICommand
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string WaiterName { get; set; }
        public int TableNumber { get; set; }
    }

    public class DrinksOrder : ICommand
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
using System;
using System.Collections.Generic;
using CQRSTutorial.Core;

namespace CQRSTutorial.Commands
{
    public class OrderDrinks : ICommand
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
using System;
using System.Collections.Generic;
using CQRSTutorial.Cafe.Events;

namespace CQRSTutorial.Cafe.Commands
{
    public class PlaceOrderCommand
    {
        public Guid Id { get; set; }
        public List<OrderedItem> Items { get; set; }
    }
}

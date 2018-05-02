using System;
using System.Collections.Generic;
using System.Text;
using CQRSTutorial.Cafe.Events;

namespace CQRSTutorial.Cafe.Domain.Commands
{
    public class PlaceOrderCommand
    {
        public Guid Id { get; set; }
        public List<OrderedItem> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSTutorial.Cafe.Events
{
    public class OrderedItem
    {
        public int MenuNumber { get; set; }
        public string Description { get; set; }
        public bool IsDrink { get; set; }
        public decimal Price { get; set; }
    }

    public class DrinksOrdered
    {
        public Guid Id { get; set; }
        public List<OrderedItem> Items { get; set; }
    }
}

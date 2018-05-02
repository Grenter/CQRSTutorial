using System;

namespace CQRSTutorial.Cafe.Events
{
    public class TabOpened
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; }
        public string Waiter { get; set; }
    }
}

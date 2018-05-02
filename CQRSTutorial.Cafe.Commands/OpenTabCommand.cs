using System;

namespace CQRSTutorial.Cafe.Commands
{
    public class OpenTabCommand
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; }
        public string Waiter { get; set; }
    }
}

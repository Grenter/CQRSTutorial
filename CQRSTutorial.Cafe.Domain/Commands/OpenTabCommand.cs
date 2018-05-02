using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSTutorial.Cafe.Domain.Commands
{
    public class OpenTabCommand
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; }
        public string Waiter { get; set; }
    }
}

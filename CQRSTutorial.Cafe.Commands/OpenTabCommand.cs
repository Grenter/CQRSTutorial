using System;
using CQRSTutorial.Cafe.Common;

namespace CQRSTutorial.Cafe.Commands
{
    public class OpenTabCommand : ICommand
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; }
        public string Waiter { get; set; }
    }
}

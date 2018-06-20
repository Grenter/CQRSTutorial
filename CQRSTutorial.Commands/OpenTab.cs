using System;
using CQRSTutorial.Core;

namespace CQRSTutorial.Commands
{
    public class OpenTab : ICommand
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string WaiterName { get; set; }
        public int TableNumber { get; set; }
    }
}
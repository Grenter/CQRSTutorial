using System;

namespace CQRSTutorial.Cafe.Commands
{
    public class CloseTabCommand
    {
        public Guid Id { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
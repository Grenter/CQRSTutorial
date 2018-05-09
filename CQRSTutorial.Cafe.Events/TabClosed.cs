using System;

namespace CQRSTutorial.Cafe.Events
{
    public class TabClosed
    {
        public Guid Id { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal OrderValue { get; set; }
        public decimal TipValue { get; set; }
    }
}
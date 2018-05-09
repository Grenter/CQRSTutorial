using System;

namespace CQRSTutorial.Cafe.Events
{
    public class TabClosed
    {
        public Guid Id { get; set; }
        public decimal AmmountPaid { get; set; }
        public decimal OrderValue { get; set; }
        public decimal TipValue { get; set; }
    }
}
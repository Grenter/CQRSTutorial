using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSTutorial.Cafe.Events
{
    public class DrinksServed
    {
        public Guid Id { get; set; }
        public List<int> MenuNumbers { get; set; }
    }
}

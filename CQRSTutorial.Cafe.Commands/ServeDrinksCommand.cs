using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSTutorial.Cafe.Commands
{
    public class ServeDrinksCommand
    {
        public Guid Id { get; set; }
        public List<int> MenuNumbers { get; set; }
    }
}

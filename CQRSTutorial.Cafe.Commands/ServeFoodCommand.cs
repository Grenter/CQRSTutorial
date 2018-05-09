using System;
using System.Collections.Generic;

namespace CQRSTutorial.Cafe.Commands
{
    public class ServeFoodCommand
    {
        public Guid Id { get; set; }
        public List<int> MenuNumbers { get; set; }
    }
    public class PrepareFoodCommand
    {
        public Guid Id { get; set; }
        public List<int> MenuNumbers { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace CQRSTutorial.Cafe.Events
{
    public class FoodServed
    {
        public Guid Id { get; set; }
        public List<int> MenuNumbers { get; set; }
    }

    public class FoodPrepared
    {
        public Guid Id { get; set; }
        public List<int> MenuNumbers { get; set; }
    }
}
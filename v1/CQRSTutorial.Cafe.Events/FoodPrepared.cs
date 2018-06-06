using System;
using System.Collections.Generic;

namespace CQRSTutorial.Cafe.Events
{
    public class FoodPrepared
    {
        public Guid Id { get; set; }
        public List<int> MenuNumbers { get; set; }
    }
}
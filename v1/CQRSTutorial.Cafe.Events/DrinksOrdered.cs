﻿using System;
using System.Collections.Generic;

namespace CQRSTutorial.Cafe.Events
{
    public class DrinksOrdered
    {
        public Guid Id { get; set; }
        public List<OrderedItem> Items { get; set; }
    }
}
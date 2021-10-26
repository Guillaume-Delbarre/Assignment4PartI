﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4PartI.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Required { get; set; }
        public IList<OrderDetails> OrderDetails { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
    }
}

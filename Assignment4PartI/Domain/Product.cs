﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4PartI.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string CategoryName
        {
            get
            {
                return Category.Name;
            }
        }

        public string ProductName
        {
            get
            {
                return Name;
            }
        }

        public string QuantityPerUnit { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, Name = {Name}, Category = {Category}";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BasketItems
    {
        public int Id { get; set; }
        public Offers Offers { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Order Order { get; set; }
    }
}

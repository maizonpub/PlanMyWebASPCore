using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BasketItems
    {
        public int Id { get; set; }
        public Offers Offers { get; set; }
        public string Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

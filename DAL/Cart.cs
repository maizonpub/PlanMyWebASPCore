using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Cart
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
    }
}

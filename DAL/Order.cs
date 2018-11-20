using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Order
    {
        public int Id { get; set; }
        public Users Users { get; set; }
        public string Status { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionUUID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Total { get; set; }
        List<BasketItems> BasketItems { get; set; }
    }
}

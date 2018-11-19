using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class Offers
    {
        public int Id { get; set; }
        public OffersType OffersType { get;set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public Validity Validity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
    public enum OffersType
    {
        HotDeals,
        Bundles
    }

    public enum Validity
    {
        Valid,
        NotValid
    }
}

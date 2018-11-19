using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class OffersCategory
    {
        public int Id { get; set; }
        public Offers Offers { get; set; }
        public VendorCategory VendorCategory { get; set; }
    }
}

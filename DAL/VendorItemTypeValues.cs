using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
     public class VendorItemTypeValues
    {
        public int Id { get; set; }
        public VendorType VendorType { get; set; }
        public int ItemId { get; set; }
        public List<VendorTypeValues> Values { get; set; }

    }
}

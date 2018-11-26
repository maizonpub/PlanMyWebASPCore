using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class VendorTypeValues
    {
        public int Id { get; set; }
        public VendorType VendorType { get; set; }
        public string Title { get; set; }
        public List<VendorItemTypeValues> VendorItemTypes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class VendorType
    {   
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string VendorCategory { get; set; }
        public IEnumerable<VendorItemTypeValue> VendorItemTypeValues { get; set; }
    }
}

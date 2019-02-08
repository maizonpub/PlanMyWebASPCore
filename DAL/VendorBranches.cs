using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
     public class VendorBranches
    {
        public int Id { get; set; }
        public VendorItem VendorItem { get; set; }
        public int VendorItemId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string UserId { get; set; }
    }
}

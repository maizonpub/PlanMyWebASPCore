using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class VendorsViewModel
    {
        public IEnumerable<VendorItem> VendorItems { get; set; }
        public IEnumerable<VendorCategory> VendorCategories { get; set; }
        public IEnumerable<VendorType> VendorTypes { get; set; }
    }
}

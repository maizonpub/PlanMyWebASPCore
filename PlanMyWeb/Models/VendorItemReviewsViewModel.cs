using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class VendorItemReviewsViewModel
    {
        public long Id { get; set; }
        public string comment { get; set; }
        public string author { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public int rating { get; set; }
        public DateTime DateIn { get; set; }
        public VendorItem VendorItem { get; set; }
        public int VendorItemId { get; set; }
        public ReviewStatus Status { get; set; }
    }
}

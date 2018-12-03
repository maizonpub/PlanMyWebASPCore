using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class HomeViewModel
    {
        public IEnumerable<HomeSlider> HomeSliders { get; set; }
        public IEnumerable<HomeTips> HomeTips { get; set; }
        public IEnumerable<VendorItem> FeaturedItems { get; set; }
        public List<VendorCategory> VendorCategories { get; set; }
        public List<Country> Countries { get; set; }
        public WebContent Webcontent { get; set; }
        public IEnumerable<Blog> LatestInspirations { get; set; } 
    }
}

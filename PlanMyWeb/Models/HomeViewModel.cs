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
    }
}

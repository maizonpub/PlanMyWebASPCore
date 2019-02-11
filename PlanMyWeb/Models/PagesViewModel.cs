using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class PagesViewModel
    {
        public Pages Pages { get; set; }
        public IEnumerable<Career> Careers { get; set; }
    }
}

using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class InspirationsViewModel
    {
        public Blog Blog { get; set; }
        public Blog NextPage { get; set; }
        public Blog PreviousPage { get; set; }
    }
}

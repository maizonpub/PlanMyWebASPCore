using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class OffersViewModel
    {
        public Offers Offers { get; set; }
        public Offers NextPage { get; set; }
        public Offers PreviousPage { get; set; }
    }

}

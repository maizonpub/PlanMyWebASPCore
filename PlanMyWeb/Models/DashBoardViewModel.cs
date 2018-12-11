using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class DashBoardViewModel
    {
        public IEnumerable<DAL.WishList> WishList { get; set; }
        public IEnumerable<DAL.CheckList> CheckList { get; set; }
        public IEnumerable<DAL.GuestList> GuestList { get; set; }
        public IEnumerable<DAL.Budget> Budget { get; set; }
        public DAL.Events Event { get; set; } 
    }
}

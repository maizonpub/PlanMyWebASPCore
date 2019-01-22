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
        public IEnumerable<DAL.BudgetCategory> BudgetCategories { get; set; }
        public DAL.Events Event { get; set; } 
        public string TaskTitle { get; set; }
        public DateTime TaskDate { get; set; }
        public string TaskDescription { get; set; }
        public int? TaskId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DAL.Side Side { get; set; }
        public DAL.GuestStatus RSVP { get; set; }
        public int? GuestId { get; set; }
        public DAL.GuestList Guest { get; set; }
    }
}

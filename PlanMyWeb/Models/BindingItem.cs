using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanMyWeb.Models
{
    public class BindingItem
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string FavImg { get; set; }
        public VendorItem item { get; set; }
    }
}

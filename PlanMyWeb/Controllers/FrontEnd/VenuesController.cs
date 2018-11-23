using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace PlanMyWeb.Controllers.FrontEnd
{
    public class VenuesController : Controller
    {
        private readonly DbWebContext _context;
        public VenuesController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IEnumerable<VendorItem> GetVenues()
        {
            return _context.VendorItems.Where(x => x.Categories.Where(y => y.VendorCategory.Title.ToLower().Contains("venu")).Count() > 0).AsEnumerable();
        }
    }
}
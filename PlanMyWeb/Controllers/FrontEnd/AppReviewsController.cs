using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PlanMyWeb.Controllers.FrontEnd
{
    public class AppReviewsController : Controller
    {
        private readonly DbWebContext _context;
        public AppReviewsController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index(int VendorItemId)
        {
            var vendorItem = _context.VendorItems.Where(x => x.Id == VendorItemId).Include(x => x.VendorItemReviews).SingleOrDefault();
            return View(vendorItem);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using PlanMyWeb.Models;

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
            VendorsViewModel model = new VendorsViewModel();
            model.VendorItems = GetVenues();
            model.VendorCategories = GetCategories();
            var VenueCategory = model.VendorCategories.Where(x => x.Title.ToLower().Contains("venu")).FirstOrDefault();
            if(VenueCategory !=null)
                model.VendorTypes = GetTypes(VenueCategory.Id);
            else
                model.VendorTypes = new List<VendorType>();
            return View(model);
        }

        private IEnumerable<VendorType> GetTypes(int categoryId)
        {
            return _context.VendorTypes.Where(x=>x.CategoryId == categoryId);
        }

        private IEnumerable<VendorCategory> GetCategories()
        {
            return _context.VendorCategories;
        }

        public IEnumerable<VendorItem> GetVenues()
        {
            return _context.VendorItems.Where(x => x.Categories.Where(y => y.VendorCategory.Title.ToLower().Contains("venu")).Count() > 0).AsEnumerable();
        }
    }
}
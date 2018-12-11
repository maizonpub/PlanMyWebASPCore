using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index(int[] TypeId)
        {
            VendorsViewModel model = new VendorsViewModel();
            model.VendorItems = GetVenues(TypeId);
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
            return _context.VendorTypes.Include(x => x.VendorTypeValues).Where(x=>x.VendorCategory.Id == categoryId);
        }

        private IEnumerable<VendorCategory> GetCategories()
        {
            return _context.VendorCategories;
        }

        public IEnumerable<VendorItem> GetVenues(int[] TypeId)
        {
            var items = _context.VendorItems.Include(x=>x.VendorItemTypeValues).Where(x => x.Categories.Where(y => y.VendorCategory.Title.ToLower().Contains("venu")).Count() > 0).AsEnumerable();
            if(TypeId.Length>0)
            {
                items = items.Where(x => x.VendorItemTypeValues.Where(y => TypeId.Contains(y.VendorTypeValue.Id)).Count() > 0);
            }
            return items;
        }
    }
}
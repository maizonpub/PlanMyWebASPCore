using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.FrontEnd
{
    public class VendorsController : Controller
    {
        private readonly DbWebContext _context;
        public VendorsController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index([FromQuery(Name = "CategoryId")] int? CategoryId)
        {
            VendorsViewModel model = new VendorsViewModel();
            model.VendorItems = GetVendorItems(CategoryId);
            model.VendorCategories = GetCategories();
            if (CategoryId != null)
                model.VendorTypes = GetTypes((int)CategoryId);
            else
                model.VendorTypes = new List<VendorType>();
            return View(model);
        }

        private IEnumerable<VendorType> GetTypes(int categoryId)
        {
            return _context.VendorTypes.Where(x => x.CategoryId == categoryId);
        }

        private IEnumerable<VendorCategory> GetCategories()
        {
            return _context.VendorCategories;
        }

        public IEnumerable<VendorItem> GetVendorItems(int? CategoryId)
        {
            if (CategoryId != null)
                return _context.VendorItems.Where(x => x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0);
            else
                return _context.VendorItems;
        }

    }

}

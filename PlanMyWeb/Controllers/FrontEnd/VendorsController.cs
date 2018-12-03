using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index([FromQuery(Name = "CategoryId")] int? CategoryId, int? CountryId)
        {
            VendorsViewModel model = new VendorsViewModel();
            var items = GetVendorItems(CategoryId, CountryId);
            model.VendorCategories = GetCategories();
            if (CategoryId != null)
            {
                model.VendorTypes = GetTypes((int)CategoryId);
            }
            else
                model.VendorTypes = new List<VendorType>();
            model.VendorItems = items;
            return View(model);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems.Include(x=>x.Gallery).FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            return View(vendorItem);
        }
        private IEnumerable<VendorType> GetTypes(int categoryId)
        {
            return _context.VendorTypes.Where(x => x.CategoryId == categoryId);
        }

        private IEnumerable<VendorCategory> GetCategories()
        {
            return _context.VendorCategories;
        }

        public IEnumerable<VendorItem> GetVendorItems(int? CategoryId, int? CountryId)
        {
            var items = new List<VendorItem>().AsEnumerable();
            if (CategoryId != null)
                items =  _context.VendorItems.Where(x => x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0);
            else
                items = _context.VendorItems;
            if(CountryId!=null)
            {
                items = items.Where(x => x.Country.Id == CountryId);
            }
            return items;
        }

    }

}

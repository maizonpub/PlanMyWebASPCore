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
            public IActionResult Index()
            {
                VendorsViewModel vendormodel = new VendorsViewModel();
                vendormodel.VendorItems = GetVendorItems();
                vendormodel.VendorCategories = GetVendorCategories();
                return View(vendormodel);
            }
            public IEnumerable<VendorItem> GetVendorItems()
            {
                return _context.VendorItems;
            }
            public IEnumerable<VendorCategory> GetVendorCategories()
            {
                return _context.VendorCategories;
            }
            
    }

}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.FrontEnd
{
    public class VendorsController : Controller
    {
        private readonly DbWebContext _context;
        protected int PageSize = 20;
        public VendorsController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? CategoryId, int? CountryId, int[] TypeId, int page = 1)
        {
            VendorsViewModel model = new VendorsViewModel();
            var items = GetVendorItems(CategoryId, CountryId, TypeId, page);
            model.VendorCategories = GetCategories();
            model.VendorTypes = GetTypes(CategoryId);
            model.VendorItems = items;
            return View(model);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var vendorItem = await _context.VendorItems.Include(x=>x.Gallery).Include(x=>x.VendorItemReviews).Include(x=>x.User).Include(x=>x.Categories).FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItem == null)
            {
                return NotFound();
            }
            var relateditems = _context.VendorItems.Where(x => x.Categories.Where(y=>vendorItem.Categories.Where(z=>z.VendorCategory == y.VendorCategory).Count()>0 && y.VendorItem!=vendorItem).Count()>0).OrderBy(x=>Guid.NewGuid()).Take(3);
            var relatedOffers = _context.Offers.Where(x => x.User.Id == vendorItem.User.Id);
            SingleVendorViewModel single = new SingleVendorViewModel { VendorItem = vendorItem, RelatedItems = relateditems, RelatedOffers = relatedOffers };
            return View(single);
        }
        private IEnumerable<VendorType> GetTypes(int? categoryId)
        {
            return _context.VendorTypes.Include(x=>x.VendorTypeValues).Where(x => x.VendorCategory.Id == categoryId);
        }

        private IEnumerable<VendorCategory> GetCategories()
        {
            return _context.VendorCategories;
        }

        public IPagedList<VendorItem> GetVendorItems(int? CategoryId, int? CountryId, int[] TypeId, int page)
        {
            var items = new List<VendorItem>().ToPagedList();
            if (CategoryId != null)
            {
                if (TypeId != null && TypeId.Length > 0)
                    items = _context.VendorItems.Include(x => x.VendorItemTypeValues).Where(x => x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0 && x.VendorItemTypeValues.Where(y => TypeId.Contains(y.VendorTypeValue.Id)).Count() > 0).ToPagedList(page,PageSize);
                else
                    items = _context.VendorItems.Include(x => x.VendorItemTypeValues).Where(x => x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0).ToPagedList(page, PageSize);
            }
            else
            {
                if (TypeId != null && TypeId.Length > 0)
                    items = _context.VendorItems.Include(x=>x.VendorItemTypeValues).Where(x => x.VendorItemTypeValues.Where(y => TypeId.Contains(y.VendorTypeValue.Id)).Count() > 0).ToPagedList(page, PageSize);
                else
                    items = _context.VendorItems.Include(x => x.VendorItemTypeValues).ToPagedList(page, PageSize);
            }
            if(CountryId!=null)
            {
                items = items.Where(x => x.Country.Id == CountryId).ToPagedList(page, PageSize);
            }
            return items;
        }
       
    }

}

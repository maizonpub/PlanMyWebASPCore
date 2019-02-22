using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using PlanMyWeb.Models;
using X.PagedList;

namespace PlanMyWeb.Controllers.FrontEnd
{
    public class OffersController : Controller
    {
        private readonly DbWebContext _context;
        protected int PageSize = 20;
        public OffersController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? categoryId, OffersType? area, int page = 1)
        {
            var offers = new List<Offers>();
            if (categoryId == null)
                offers = _context.Offers.OrderByDescending(x => x.OffersType).ToList();
            else
                offers = _context.Offers.Where(x => x.OffersCategories.Where(y => y.VendorCategory.Id == categoryId).Count() > 0).OrderByDescending(x => x.OffersType).ToList();
            if(area!=null)
            {
                offers = offers.Where(x => x.OffersType == (OffersType)area).ToList();
            }
            
            return View(offers.ToPagedList(page, PageSize));
        }
        public IActionResult Details(int id)
        {
            OffersViewModel model = new OffersViewModel();
            var offer = _context.Offers.Where(x => x.Id == id).SingleOrDefault();
            model.Offers = offer;
            return View(model);
        }
    }
}

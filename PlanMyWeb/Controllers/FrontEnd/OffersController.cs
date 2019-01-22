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
        public IActionResult Index(int? categoryId, int page = 1)
        {
            var offers = new List<Offers>().ToPagedList();
            if (categoryId == null)
                offers = _context.Offers.OrderByDescending(x => x.OffersType).ToPagedList(page, PageSize);
            else
                offers = _context.Offers.Where(x => x.OffersCategories.Where(y => y.VendorCategory.Id == categoryId).Count() > 0).OrderByDescending(x => x.OffersType).ToPagedList(page, PageSize);
            return View(offers);
        }
        public IActionResult Details(int id)
        {
            PlanMyWeb.Models.OffersViewModel model = new PlanMyWeb.Models.OffersViewModel();
            var offer = _context.Offers.Where(x => x.Id == id).SingleOrDefault();
            model.Offers = offer;
            return View(model);
        }
    }
}

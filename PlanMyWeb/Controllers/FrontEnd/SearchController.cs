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
    public class SearchController : Controller
    {
        protected DbWebContext _context;
        public SearchController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index(string s)
        {
            ViewData["Search"] = s;
            List<SearchViewModel> sv = new List<SearchViewModel>();
            string lower = s.ToLower();
            var items = _context.VendorItems.Include(x=>x.Gallery).Where(x => x.HtmlDescription.ToLower().Contains(lower) || x.Title.ToLower().Contains(lower)|| x.Address.ToLower().Contains(lower) || x.Categories.Where(y=>y.VendorCategory.Title.ToLower().Contains(lower)).Count()>0 || x.Country.Name.ToLower().Contains(lower) || x.Country.Code.ToLower().Contains(lower));
            foreach(var item in items)
            {
                string thumb = item.Thumb;
                sv.Add(new SearchViewModel { Id = item.Id, SearchType = SearchType.Vendors, Text = item.HtmlDescription, Title = item.Title, Thumb = thumb });
            }
            var blogs = _context.Blogs.Where(x => x.Title.ToLower().Contains(lower) || x.HtmlDescription.ToLower().Contains(lower) || x.BlogCategoryRelations.Where(y => y.Category.Title.ToLower().Contains(lower)).Count() > 0);
            foreach(var item in blogs)
            {
                string thumb = item.Image;
                sv.Add(new SearchViewModel { Id = item.Id, SearchType = SearchType.Blog, Text = item.HtmlDescription, Title = item.Title, Thumb = thumb });
            }
            return View(sv);
        }
    }
}
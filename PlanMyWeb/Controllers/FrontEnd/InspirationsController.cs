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
    public class InspirationsController : Controller
    {
        private readonly DbWebContext _context;
        public InspirationsController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? CategoryId)
        {
           
            var blogs = _context.Blogs.OrderByDescending(x => x.PostDate).AsEnumerable();
            var categories = _context.BlogCategories.ToList();
            if (CategoryId != null)
                //blogs = blogs.Where(x => x.BlogCategoryRelations == (BlogCategoryRelation)CategoryId).AsEnumerable();
            blogs = blogs.Where(x => x.BlogCategoryRelations!= null && x.BlogCategoryRelations.Where(y => y.Category.Id == (int)CategoryId).Count() > 0).AsEnumerable();
            InspirationsListViewModel model = new InspirationsListViewModel { Blogs = blogs, Categories = categories };
            model.BlogGalleries = GetBlogGalleries();
            return View(model);
        }

        private IEnumerable<BlogGallery> GetBlogGalleries()
        {
            return _context.BlogGalleries;
        }

        public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        InspirationsViewModel model = new InspirationsViewModel();
        var blog = await _context.Blogs.Include(x=>x.Gallery).FirstOrDefaultAsync(m => m.Id == id);
        if (blog == null)
        {
            return NotFound();
        }
        model.Blog = blog;
        model.PreviousPage = _context.Blogs.Where(x => x.Id < id).OrderByDescending(x => x.Id).FirstOrDefault();
        model.NextPage = _context.Blogs.Where(x => x.Id > id).OrderBy(x => x.Id).FirstOrDefault();
        return View(model);
    }
    }
}
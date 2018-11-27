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
        public IActionResult Index()
        {
            return View(_context.Blogs.OrderByDescending(x=>x.PostDate));
        }
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        InspirationsViewModel model = new InspirationsViewModel();
        var blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
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
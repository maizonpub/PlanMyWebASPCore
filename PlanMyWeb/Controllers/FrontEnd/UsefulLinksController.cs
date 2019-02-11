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
    public class UsefulLinksController : Controller
    {
        private readonly DbWebContext _context;
        public UsefulLinksController(DbWebContext context)
        {
            _context = context;
        }
        [HttpGet("Pages/{title}")]
        public IActionResult Index([FromRoute] string title)
        {
            var page = _context.Pages.Where(x => x.Title == title).FirstOrDefault();
            if(page==null)
            {
                return NotFound();
            }
            var careers = _context.Careers.Where(x => x.Pages.Id == page.Id).AsEnumerable();
            PagesViewModel model = new PagesViewModel { Pages = page, Careers = careers };
            return View(model);
        }
    }

}
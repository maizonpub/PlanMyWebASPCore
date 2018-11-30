using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace PlanMyWeb.Controllers.FrontEnd
{
    public class ContactusController : Controller
    {
        DbWebContext _context;
        public ContactusController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.WebContents.FirstOrDefault());
        }
    }
}
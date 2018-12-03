using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.FrontEnd
{
    [Authorize(Roles = "Admin,Vendor,Planner")]
    public class DashBoardController : Controller
    {
        private readonly DbWebContext _context;
        public DashBoardController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CheckList()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.FrontEnd
{
    [Authorize(Roles = "Admin,Vendor,Planner")]
    public class DashBoardController : Controller
    {
        private readonly DbWebContext _context;
        private readonly UserManager<DAL.Users> _userManager;
        protected IEnumerable<WishList> wishlist;
        protected IEnumerable<CheckList> checklist;
        protected IEnumerable<GuestList> guestLists;
        protected IEnumerable<Budget> budgets;
        protected Events Event;
        public DashBoardController(DbWebContext context, UserManager<DAL.Users> userManager)
        {
            _context = context;
            _userManager = userManager;
            
            
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            wishlist = _context.WishLists.Where(x => x.User.Id == userId);
            Event = _context.Events.Where(x => x.User.Id == userId).OrderByDescending(x => x.Id).FirstOrDefault();
            DashBoardViewModel model = new DashBoardViewModel { WishList = wishlist, Event = Event };
            return View(model);
        }
        public IActionResult CheckList()
        {
            var userId = _userManager.GetUserId(User);
            checklist = _context.CheckLists.Where(x => x.User.Id == userId).OrderByDescending(x => x.Id);
            DashBoardViewModel model = new DashBoardViewModel { CheckList = checklist};
            return View(model);
        }
        public IActionResult GuestList()
        {
            var userId = _userManager.GetUserId(User);
            guestLists = _context.GuestLists.Where(x => x.User.Id == userId).OrderByDescending(x => x.Id);
            DashBoardViewModel model = new DashBoardViewModel { GuestList = guestLists };
            return View(model);
        }
        public IActionResult MyBudget()
        {
            var userId = _userManager.GetUserId(User);
            budgets = _context.Budgets.Where(x => x.User.Id == userId).OrderByDescending(x => x.Id);
            DashBoardViewModel model = new DashBoardViewModel { Budget = budgets };
            return View(model);
        }
        public IActionResult SeatingChart()
        {
            return View();
        }
        public IActionResult WishList()
        {
            var userId = _userManager.GetUserId(User);
            wishlist = _context.WishLists.Where(x => x.User.Id == userId);
            DashBoardViewModel model = new DashBoardViewModel { WishList = wishlist };
            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        protected IEnumerable<GuestListTables> guestListTables;
        protected IEnumerable<BudgetCategory> budgets;
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
        public async Task<IActionResult> CheckList(int? edit, string action, string todotitle, DateTime? tododate, string tododetail, int? todoId, int? status)
        {
            var userId = _userManager.GetUserId(User);
            
            DashBoardViewModel model = new DashBoardViewModel();
            if (edit!=null)
            {
                var todo = _context.CheckLists.Where(x => x.Id == edit).SingleOrDefault();
                model.TaskTitle = todo.Title;
                model.TaskDescription = todo.Description;
                model.TaskDate = todo.Timing;
                model.TaskId = todo.Id;
            }
            if(action=="edittask")
            {
                var todo = _context.CheckLists.Where(x => x.Id == todoId).SingleOrDefault();
                todo.Title = todotitle;
                todo.Description = tododetail;
                todo.Timing = tododate != null ? (DateTime)tododate : todo.Timing;
                _context.SaveChanges();
            }
            if (action == "add")
            {
                var user = await _userManager.GetUserAsync(User);
                _context.CheckLists.Add(new DAL.CheckList { Description = tododetail, Timing = tododate != null ? (DateTime)tododate : DateTime.Now, Title = todotitle, User = user, Status = CheckListStatus.ToDo });
                _context.SaveChanges();
            }
            else if (action == "updatestatus")
            {
                var todo = _context.CheckLists.Where(x => x.Id == todoId).SingleOrDefault();
                if (todo == null)
                {
                    return NotFound();
                }
                else
                {
                    todo.Status = status == 0 ? CheckListStatus.ToDo : CheckListStatus.Done;
                    _context.SaveChanges();
                }
            }
            else if (action == "delete")
            {
                var todo = _context.CheckLists.Where(x => x.Id == todoId).SingleOrDefault();
                if (todo == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.CheckLists.Remove(todo);
                    _context.SaveChanges();
                }
            }
            checklist = _context.CheckLists.Where(x => x.User.Id == userId).OrderByDescending(x => x.Id);
            model.CheckList = checklist;
            return View(model);
        }
        public async Task<IActionResult> GuestList(int? edit, string action, string guest_name, string Address, string Country, string Email, Side Side, string City, string Phone, GuestStatus RSVP, int? guestId)
        {
            var user = await _userManager.GetUserAsync(User);
            DashBoardViewModel model = new DashBoardViewModel();
            
            if (edit != null)
            {
                var guest = _context.GuestLists.Where(x => x.Id == edit).SingleOrDefault();
                model.GuestId = edit;
                model.Guest = guest;
            }
            if (action== "addguest")
            {
                _context.GuestLists.Add(new DAL.GuestList { Address = Address, Email = Email, FullName = guest_name, Side = Side, GuestStatus = RSVP, Phone = Phone, User = user, Country = Country, City = City });
                _context.SaveChanges();
            }
            else if(action=="editguest")
            {
                var guest = _context.GuestLists.Where(x => x.Id == guestId).SingleOrDefault();
                if(guest == null)
                {
                    return NotFound();
                }
                else
                {
                    guest.Address = Address;
                    guest.Email = Email;
                    guest.FullName = guest_name;
                    guest.Side = Side;
                    guest.GuestStatus = RSVP;
                    guest.Phone = Phone;
                    guest.City = City;
                    guest.Country = Country;
                    _context.SaveChanges();
                }
            }
            else if (action == "deleteguest")
            {
                var guest = _context.GuestLists.Where(x => x.Id == guestId).SingleOrDefault();
                if (guest == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.GuestLists.Remove(guest);
                    _context.SaveChanges();
                }
            }
            guestLists = _context.GuestLists.Where(x => x.User.Id == user.Id).OrderByDescending(x => x.Id);
            model.GuestList = guestLists;
            return View(model);
        }
        public async Task<IActionResult> MyBudget(int? edit, string action, string budget_category, string item_name, decimal item_estimate, decimal item_actual, decimal item_paid, int item_category, int itemid)
        {
            
            var user = await _userManager.GetUserAsync(User);
            DashBoardViewModel model = new DashBoardViewModel();
            if (edit != null)
            {
                var category = _context.BudgetCategories.Where(x => x.Id == edit).SingleOrDefault();
                model.CategoryName = category.Title;
                model.CategoryId = edit;
            }
            if(action=="editcategory")
            {
                var category = _context.BudgetCategories.Where(x => x.Id == item_category).SingleOrDefault();
                category.Title = budget_category;
                _context.SaveChanges();
            }
            if (action == "addcategory")
            {

                _context.BudgetCategories.Add(new BudgetCategory { Title = budget_category, User = user });
                _context.SaveChanges();
            }
            else if (action == "addbudgetitem")
            {
                _context.Budgets.Add(new Budget { ActualCost = item_actual, BudgetCategoryId = item_category, Description = item_name, EstimatedCost = item_estimate, PaidCost = item_paid, User = user });
                _context.SaveChanges();
            }
            else if (action == "editbudgetitem")
            {
                var budget = _context.Budgets.Where(x => x.Id == itemid).SingleOrDefault();
                if (budget == null)
                {
                    return NotFound();
                }
                else
                {
                    budget.ActualCost = item_actual;
                    budget.PaidCost = item_paid;
                    budget.EstimatedCost = item_estimate;
                    budget.Description = item_name;
                    _context.SaveChanges();
                }
            }
            else if (action == "deletebudgetitem")
            {
                var budget = _context.Budgets.Where(x => x.Id == itemid).SingleOrDefault();
                if (budget == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Budgets.Remove(budget);
                    _context.SaveChanges();
                }
            }
            else if (action == "deletebudgetcategory")
            {
                var budgetcategory = _context.BudgetCategories.Where(x => x.Id == item_category).SingleOrDefault();
                if (budgetcategory == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.BudgetCategories.Remove(budgetcategory);
                    _context.SaveChanges();
                }
            }
            budgets = _context.BudgetCategories.Include(x=>x.Budgets).Where(x => x.User.Id == user.Id).OrderByDescending(x => x.Id);
            model.BudgetCategories = budgets;
            return View(model);
        }
        public async Task<IActionResult>SeatingChart(int? edit, string action, string tableName, TableType type, string seatsNumber, int? seatId)
        {
            var user = await _userManager.GetUserAsync(User);
            DashBoardViewModel model = new DashBoardViewModel();

            if (edit != null)
            {
                var seat = _context.GuestListTables.Where(x => x.Id == edit).SingleOrDefault();
                model.TableId = edit;
                //model.GuestListTables = seat;
            }
            if (action == "addTable")
            {
                _context.GuestListTables.Add(new DAL.GuestListTables { TableName = tableName, TableType = type, SeatsNumber = seatsNumber,  User = user});
                _context.SaveChanges();
            }
            else if (action == "editTable")
            {
                var seat = _context.GuestListTables.Where(x => x.Id == seatId).SingleOrDefault();
                if (seat == null)
                {
                    return NotFound();
                }
                else
                {
                    seat.TableName = tableName;
                    seat.TableType = type;
                    seat.SeatsNumber = seatsNumber;
                    _context.SaveChanges();
                }
            }
            else if (action == "deleteTable")
            {
                var seat = _context.GuestListTables.Where(x => x.Id == seatId).SingleOrDefault();
                if (seat == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.GuestListTables.Remove(seat);
                    _context.SaveChanges();
                }
            }
            guestListTables = _context.GuestListTables.Where(x => x.User.Id == user.Id).OrderByDescending(x => x.Id);
            model.GuestListTables = guestListTables;
            return View(model);
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
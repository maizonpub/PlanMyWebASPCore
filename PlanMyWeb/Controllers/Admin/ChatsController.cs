using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin,Vendor")]
    public class ChatsController : Controller
    {
        private readonly DbWebContext _context;
        private readonly UserManager<DAL.Users> _userManager;
        public ChatsController(DbWebContext context, UserManager<DAL.Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route("Admin/Chats")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

    }
}

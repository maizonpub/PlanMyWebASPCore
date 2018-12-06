using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PlanMyWeb.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/WebContents");
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
        }
        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
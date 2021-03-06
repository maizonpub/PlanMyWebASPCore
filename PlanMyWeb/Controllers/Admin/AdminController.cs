﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {

                if (User.IsInRole("Admin"))
                    return Redirect("/Admin/WebContents");
                else
                    return Redirect("/");
            }
            else
            {
                return Redirect("/Identity/Account/Login?returnUrl=/Admin/WebContents");
            }
        }
        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
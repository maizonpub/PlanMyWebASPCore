using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbWebContext _context;
        public HomeController(DbWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homemodel = new HomeViewModel();
            homemodel.HomeSliders = GetHomeSlider();
            homemodel.HomeTips = GetHomeTips();
            homemodel.FeaturedItems = GetFeaturedItems();
            homemodel.Webcontent = GetWebContent();
            homemodel.LatestInspirations = GetLatestInspirations();
            return View(homemodel);
        }
        
        public IEnumerable<HomeSlider> GetHomeSlider()
        {
            return _context.HomeSlider;
        }
        public IEnumerable<HomeTips> GetHomeTips()
        {
            return _context.HomeTips;
        }
        public IEnumerable<VendorItem> GetFeaturedItems()
        {
            return _context.VendorItems.Where(x=>x.IsFeatured == true);
        }
        public IEnumerable<Blog> GetLatestInspirations()
        {
            return _context.Blogs.OrderByDescending(x=>x.PostDate);
        }
        public WebContent GetWebContent()
        {
            return _context.WebContents.FirstOrDefault();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

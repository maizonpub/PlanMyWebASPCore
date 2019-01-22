using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Authorization;
using PlanMyWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminOffersController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdminOffersController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/Offers")]
        // GET: AdminOffers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Offers.ToListAsync());
        }
        [Route("Admin/Offers/Details/{id?}")]
        // GET: AdminOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offers = await _context.Offers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offers == null)
            {
                return NotFound();
            }

            return View(offers);
        }
        [Route("Admin/Offers/Create")]
        // GET: AdminOffers/Create
        public IActionResult Create()
        {
            var vendorcategoies = _context.VendorCategories.AsEnumerable();
            var users = _context.Users.AsEnumerable();
            var userselect = new SelectList(users, "Id", "FirstName");
            ViewBag.Users = userselect;
            ViewBag.Categories = new SelectList(vendorcategoies, "Id", "Title");
            return View();
        }

        // POST: AdminOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Offers/Create")]
        public async Task<IActionResult> Create([Bind("Id,OffersType,Image,Title,Description,Validity,StartDate,EndDate,Price,SalePrice,SaleFromDate,SaleToDate,Categories")] AdminOffersViewModel offers)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (offers.Image != null && offers.Image.Length > 0)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + offers.Image.FileName;
                    UploadFile(offers.Image, filename);
                }
                string userId = Request.Form["User"];
                var user = _context.Users.Find(userId);
                var Offer = new Offers { Description = offers.Description, EndDate = offers.EndDate, OffersType = offers.OffersType, Image = filename, Price = offers.Price, SaleFromDate = offers.SaleFromDate, SalePrice = offers.SalePrice, SaleToDate = offers.SaleToDate, StartDate = offers.SaleToDate, Title = offers.Title, Validity = offers.Validity, User = user };
                _context.Offers.Add(Offer);
                string[] catIds = Request.Form["Categories"].ToString().Split(',');

                var dbcats = _context.VendorCategories.Where(x => catIds.Contains(x.Id.ToString())).ToList();
                foreach (var dbcat in dbcats)
                    _context.OffersCategories.Add(new OffersCategory { VendorCategory = dbcat, Offers = Offer });
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offers);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        // GET: AdminOffers/Edit/5
        [Route("Admin/Offers/Edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = _context.Users.AsEnumerable();
            var userselect = new SelectList(users, "Id", "FirstName");
            var offers = _context.Offers.Include(x=>x.OffersCategories).Where(x=>x.Id == id).FirstOrDefault();
            var vendorcategoies = _context.VendorCategories.AsEnumerable();
            var catslist = new SelectList(vendorcategoies, "Id", "Title");
            foreach (var item in catslist)
            {
                foreach (var itemcategory in offers.OffersCategories)
                {
                    if (item.Value == itemcategory.VendorCategory.Id.ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
            foreach (var item in userselect)
            {

                if (item.Value == offers.UserId)
                {
                    item.Selected = true;
                }
            }
            AdminOffersViewModel model = new AdminOffersViewModel { Categories = catslist, Description = offers.Description, EndDate = offers.EndDate, Id = offers.Id, OffersType = offers.OffersType, Price = offers.Price, SaleFromDate = offers.SaleFromDate, SalePrice = offers.SalePrice, SaleToDate = offers.SaleToDate, StartDate = offers.StartDate, Title = offers.Title, Validity = offers.Validity, User = userselect };
            if (offers == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: AdminOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Offers/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OffersType,Image,Title,Description,Validity,StartDate,EndDate,Price,SalePrice,SaleFromDate,SaleToDate,Categories")] AdminOffersViewModel offers)
        {
            var row = _context.Offers.Include(x => x.OffersCategories).Where(x => x.Id == id).FirstOrDefault();
            string userId = Request.Form["User"];
            

            if (ModelState.IsValid)
            {
                if (offers.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + offers.Image.FileName;
                    UploadFile(offers.Image, filename);
                }
                else
                    row.Image = row.Image;
                row.Description = offers.Description;
                row.UserId = !string.IsNullOrEmpty(userId)?userId:null;
                row.EndDate = offers.EndDate;
                row.OffersType = offers.OffersType;
                row.Price = offers.Price;
                row.SaleFromDate = offers.SaleFromDate;
                row.SalePrice = offers.SalePrice;
                row.SaleToDate = offers.SaleToDate;
                row.StartDate = offers.StartDate;
                row.Title = offers.Title;
                row.Validity = offers.Validity;
                _context.OffersCategories.RemoveRange(row.OffersCategories);
                string[] catIds = Request.Form["Categories"].ToString().Split(',');

                var dbcats = _context.VendorCategories.Where(x => catIds.Contains(x.Id.ToString())).ToList();
                foreach (var dbcat in dbcats)
                    _context.OffersCategories.Add(new OffersCategory { VendorCategory = dbcat, Offers = row });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offers);
        }

        // GET: AdminOffers/Delete/5
        [Route("Admin/Offers/Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offers = await _context.Offers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offers == null)
            {
                return NotFound();
            }

            return View(offers);
        }

        // POST: AdminOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Offers/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offers = await _context.Offers.FindAsync(id);
            _context.Offers.Remove(offers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffersExists(int id)
        {
            return _context.Offers.Any(e => e.Id == id);
        }
    }

    
}

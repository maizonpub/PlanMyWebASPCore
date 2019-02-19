using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using PlanMyWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin,Vendor")]
    public class VendorItemsController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<Users> _userManager;
        public VendorItemsController(DbWebContext context, IHostingEnvironment hostingEnvironment, UserManager<Users> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _userManager = userManager;
        }
        [Route("Admin/VendorItems")]
        // GET: VendorItems
        public async Task<IActionResult> Index()
        {
            var vendorItems = await _context.VendorItems.ToListAsync();
            if (User.IsInRole("Vendor"))
            {
                var userId = _userManager.GetUserId(User);
                vendorItems = vendorItems.Where(x => x.UserId == userId).ToList();
            }
            return View(vendorItems);
        }
        [Route("Admin/VendorItems/Details/{id?}")]
        // GET: VendorItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            return View(vendorItem);
        }
        [Route("Admin/VendorItems/Create")]
        // GET: VendorItems/Create
        public IActionResult Create()
        {
            var vendorcategories = _context.VendorCategories.AsEnumerable();
            var users = _context.Users.AsEnumerable();
            var userselect = new SelectList(users, "Id", "FirstName");
            ViewBag.Users = userselect;
            ViewBag.Categories = new SelectList(vendorcategories, "Id", "Title");
            var taxonomies = _context.VendorTypes.Include(x => x.VendorTypeValues);
            var model = new VendorItemViewModel { Taxonomies = taxonomies };
            return View(model);
        }

        // POST: VendorItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItems/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title,Latitude,Longitude,Location,PhoneNumber,HtmlDescription,Address,Email,IsFeatured,Thumb,Categories,Website,Facebook,Instagram")] VendorItemViewModel vendorItemViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (vendorItemViewModel.Thumb != null && vendorItemViewModel.Thumb.Length > 0)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + vendorItemViewModel.Thumb.FileName;
                    UploadFile(vendorItemViewModel.Thumb, filename);
                }
                string userId = "";
                if(!string.IsNullOrEmpty(Request.Form["User"]))
                    userId = Request.Form["User"];
                else
                {
                    userId = _userManager.GetUserId(User);
                }
                var user = _context.Users.Find(userId);
                VendorItem row = new VendorItem { Thumb = filename, MediaType = vendorItemViewModel.MediaType, Address = vendorItemViewModel.Address, Country = vendorItemViewModel.Country, Email = vendorItemViewModel.Email, HtmlDescription = vendorItemViewModel.HtmlDescription, IsFeatured = vendorItemViewModel.IsFeatured, Latitude = vendorItemViewModel.Latitude, Longitude = vendorItemViewModel.Longitude, Location = vendorItemViewModel.Location, PhoneNumber = vendorItemViewModel.PhoneNumber, Title = vendorItemViewModel.Title, UserId = user.Id, Website = vendorItemViewModel.Website, Facebook = vendorItemViewModel.Facebook, Instagram = vendorItemViewModel.Instagram };
                string[] catIds = Request.Form["Categories"].ToString().Split(',');
                
                    var dbcats = _context.VendorCategories.Where(x => catIds.Contains(x.Id.ToString())).ToList();
                    foreach (var dbcat in dbcats)
                        _context.VendorItemCategories.Add(new VendorItemCategory { VendorCategory = dbcat, VendorItem = row });
                var taxonomies = _context.VendorTypes.Include(x => x.VendorTypeValues);
                foreach (var tax in taxonomies)
                {
                    string[] valueIds = Request.Form[tax.Id.ToString()].ToString().Split(',');
                    
                        var dbvalues = _context.VendorTypeValues.Where(x => valueIds.Contains(x.Id.ToString())).ToList();
                        foreach (var dbcat in dbvalues)
                            _context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorTypeValue = dbcat, VendorItem = row });
                    
                }
                _context.Add(row);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItemViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        [Route("Admin/VendorItems/Edit/{id?}")]
        // GET: VendorItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = _context.Users.AsEnumerable();
            var userselect = new SelectList(users, "Id", "FirstName");
            var vendorItem = _context.VendorItems.Include(x=>x.Categories).Include(x=>x.VendorItemTypeValues).Where(x=>x.Id == id).FirstOrDefault();
            var vendorcategories = _context.VendorCategories.AsEnumerable();
            var catslist = new SelectList(vendorcategories, "Id", "Title");
            foreach (var item in catslist)
            {
                foreach (var itemcategory in vendorItem.Categories)
                {
                    if (item.Value == itemcategory.VendorCategory.Id.ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
            var taxonomies = _context.VendorTypes.Include(x => x.VendorTypeValues);
            var vendoritemtypevalues = _context.VendorItemTypeValues.Where(x => x.VendorItem == vendorItem).Include(x => x.VendorTypeValue).ToList();
            //ViewBag.Categories = selectfield;
            
            VendorItemViewModel model = new VendorItemViewModel { Id = vendorItem.Id, MediaType = vendorItem.MediaType, Address = vendorItem.Address, Country = vendorItem.Country, Email = vendorItem.Email, ItemCategories = vendorItem.Categories, Gallery = vendorItem.Gallery, HtmlDescription = vendorItem.HtmlDescription, IsFeatured = vendorItem.IsFeatured, Latitude = vendorItem.Latitude, Longitude = vendorItem.Longitude, Location = vendorItem.Location, PhoneNumber = vendorItem.PhoneNumber, Title = vendorItem.Title, User = userselect, Categories = catslist, Taxonomies = taxonomies, values = vendoritemtypevalues, Website = vendorItem.Website, Facebook = vendorItem.Facebook, Instagram = vendorItem.Instagram};
            if (vendorItem == null)
            {
                
                return NotFound();
            }
            return View(model);
        }

        // POST: VendorItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItems/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Latitude,Longitude,Location,PhoneNumber,HtmlDescription,Address,Email,IsFeatured,Thumb,Categories,Facebook, Instagram,Website")] VendorItemViewModel vendorItemViewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = "";
                if (!string.IsNullOrEmpty(Request.Form["User"]))
                    userId = Request.Form["User"];
                else
                {
                    userId = _userManager.GetUserId(User);
                }
                var user = _context.Users.Find(userId);
                var row = _context.VendorItems.Include(x=>x.Categories).Where(x => x.Id == id).FirstOrDefault();
                if (vendorItemViewModel.Thumb != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + vendorItemViewModel.Thumb.FileName;
                    UploadFile(vendorItemViewModel.Thumb, filename);
                    row.MediaType = vendorItemViewModel.MediaType;
                    row.Thumb = filename;
                }
                else
                    row.Thumb = row.Thumb;
                row.Latitude = vendorItemViewModel.Latitude;
                row.Longitude = vendorItemViewModel.Longitude;
                row.UserId = userId;
                row.Location = vendorItemViewModel.Location;
                row.PhoneNumber = vendorItemViewModel.PhoneNumber;
                row.Title = vendorItemViewModel.Title;
                row.HtmlDescription = vendorItemViewModel.HtmlDescription;
                row.Address = vendorItemViewModel.Address;
                row.Email = vendorItemViewModel.Email;
                row.Website = vendorItemViewModel.Website;
                row.Facebook = vendorItemViewModel.Facebook;
                row.Instagram = vendorItemViewModel.Instagram;
                row.IsFeatured = vendorItemViewModel.IsFeatured;
                _context.VendorItemCategories.RemoveRange(row.Categories);
                string[] catIds = Request.Form["Categories"].ToString().Split(',');
                
                    var dbcats = _context.VendorCategories.Where(x => catIds.Contains(x.Id.ToString())).ToList();
                    foreach(var dbcat in dbcats)
                        _context.VendorItemCategories.Add(new VendorItemCategory { VendorCategory = dbcat, VendorItem = row });
                _context.VendorItemTypeValues.RemoveRange(_context.VendorItemTypeValues.Where(x => x.VendorItemId == row.Id));
            var taxonomies = _context.VendorTypes.Include(x => x.VendorTypeValues);
                foreach (var tax in taxonomies)
                {
                    string[] valueIds = Request.Form[tax.Id.ToString()].ToString().Split(',');

                    var dbvalues = _context.VendorTypeValues.Where(x => valueIds.Contains(x.Id.ToString())).ToList();
                    foreach (var dbcat in dbvalues)
                        _context.VendorItemTypeValues.Add(new VendorItemTypeValue { VendorTypeValue = dbcat, VendorItem = row });

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItemViewModel);
        }
           
        
        [Route("Admin/VendorItems/Delete/{id?}")]
        // GET: VendorItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            return View(vendorItem);
        }

        // POST: VendorItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItems/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItem = await _context.VendorItems.FindAsync(id);
            _context.VendorItems.Remove(vendorItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorItemExists(int id)
        {
            return _context.VendorItems.Any(e => e.Id == id);
        }
        
    }
}

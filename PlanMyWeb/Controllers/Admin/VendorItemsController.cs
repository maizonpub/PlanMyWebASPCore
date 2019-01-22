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

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class VendorItemsController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public VendorItemsController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/VendorItems")]
        // GET: VendorItems
        public IActionResult Index()
        {
            return View(_context.VendorItems);
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
            var vendorcategoies = _context.VendorCategories.AsEnumerable();
            ViewBag.Categories = new SelectList(vendorcategoies, "Id", "Title");
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
        public async Task<IActionResult> Create([Bind("Id,Title,Latitude,Longitude,Location,PhoneNumber,HtmlDescription,Address,Email,IsFeatured,Thumb,Categories")] VendorItemViewModel vendorItemViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (vendorItemViewModel.Thumb != null && vendorItemViewModel.Thumb.Length > 0)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + vendorItemViewModel.Thumb.FileName;
                    UploadFile(vendorItemViewModel.Thumb, filename);
                }
                VendorItem row = new VendorItem { Thumb = filename, MediaType = vendorItemViewModel.MediaType, Address = vendorItemViewModel.Address, Country = vendorItemViewModel.Country, Email = vendorItemViewModel.Email, HtmlDescription = vendorItemViewModel.HtmlDescription, IsFeatured = vendorItemViewModel.IsFeatured, Latitude = vendorItemViewModel.Latitude, Longitude = vendorItemViewModel.Longitude, Location = vendorItemViewModel.Location, PhoneNumber = vendorItemViewModel.PhoneNumber, Title = vendorItemViewModel.Title };
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

            var vendorItem = _context.VendorItems.Include(x=>x.Categories).Include(x=>x.VendorItemTypeValues).Where(x=>x.Id == id).FirstOrDefault();
            var vendorcategoies = _context.VendorCategories.AsEnumerable();
            var catslist = new SelectList(vendorcategoies, "Id", "Title");
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
            VendorItemViewModel model = new VendorItemViewModel { Id = vendorItem.Id, MediaType = vendorItem.MediaType, Address = vendorItem.Address, Country = vendorItem.Country, Email = vendorItem.Email, ItemCategories = vendorItem.Categories, Gallery = vendorItem.Gallery, HtmlDescription = vendorItem.HtmlDescription, IsFeatured = vendorItem.IsFeatured, Latitude = vendorItem.Latitude, Longitude = vendorItem.Longitude, Location = vendorItem.Location, PhoneNumber = vendorItem.PhoneNumber, Title = vendorItem.Title, User = vendorItem.User, Categories = catslist, Taxonomies = taxonomies, values = vendoritemtypevalues };
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Latitude,Longitude,Location,PhoneNumber,HtmlDescription,Address,Email,IsFeatured,Thumb,Categories")] VendorItemViewModel vendorItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.VendorItems.Include(x=>x.Categories).Where(x => x.Id == id).FirstOrDefault();
                if (vendorItemViewModel.Thumb != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + vendorItemViewModel.Thumb.FileName;
                    UploadFile(vendorItemViewModel.Thumb, filename);
                    row.MediaType = vendorItemViewModel.MediaType;
                }
                else
                    row.Thumb = row.Thumb;
                row.MediaType = vendorItemViewModel.MediaType;
                row.Latitude = vendorItemViewModel.Latitude;
                row.Longitude = vendorItemViewModel.Latitude;
                row.PhoneNumber = vendorItemViewModel.PhoneNumber;
                row.Title = vendorItemViewModel.Title;
                row.HtmlDescription = vendorItemViewModel.HtmlDescription;
                row.Address = vendorItemViewModel.Address;
                row.IsFeatured = vendorItemViewModel.IsFeatured;
                _context.VendorItemCategories.RemoveRange(row.Categories);
                string[] catIds = Request.Form["Categories"].ToString().Split(',');
                
                    var dbcats = _context.VendorCategories.Where(x => catIds.Contains(x.Id.ToString())).ToList();
                    foreach(var dbcat in dbcats)
                        _context.VendorItemCategories.Add(new VendorItemCategory { VendorCategory = dbcat, VendorItem = row });
                
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

﻿using System;
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
using Microsoft.AspNetCore.Identity;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin,Vendor")]
    public class VendorItemGalleriesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<Users> _userManager;
        public VendorItemGalleriesController(DbWebContext context, IHostingEnvironment hostingEnvironment, UserManager<Users> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _userManager = userManager;
        }
    
        [Route("Admin/VendorItemGalleries")]
        // GET: VendorItemGalleries
        public async Task<IActionResult> Index(int itemId)
        {
            var vendorItemGalleries = await _context.VendorItemGalleries.Where(x=>x.Item.Id == itemId).ToListAsync();
            if (User.IsInRole("Vendor"))
            {
                var userId = _userManager.GetUserId(User);
                vendorItemGalleries = vendorItemGalleries.Where(x => x.UserId == userId).ToList();
            }
            ViewBag.ItemId = itemId.ToString();
            return View(vendorItemGalleries);
           
        }
        [Route("Admin/VendorItemGalleries/Details/{id?}")]
        // GET: VendorItemGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = await _context.VendorItemGalleries.Include(x => x.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.ItemId = vendorItemGallery.Item.Id.ToString();
            if (vendorItemGallery == null)
            {
                return NotFound();
            }

            return View(vendorItemGallery);
        }
        [Route("Admin/VendorItemGalleries/Create")]
        // GET: VendorItemGalleries/Create
        public IActionResult Create(string itemId)
        {
            ViewBag.ItemId = itemId;
            return View();
        }

        // POST: VendorItemGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemGalleries/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image")] VendorItemGalleryViewModel vendorItemGalleryViewModel, int itemId)
        {
            if (ModelState.IsValid)
            {
                foreach (var img in vendorItemGalleryViewModel.Image)
                {
                    string filename = "";
                    if (img != null)
                    {
                        filename = Guid.NewGuid().ToString().Substring(4) + img.FileName;
                        UploadFile(img, filename);
                    }

                    string userId = "";
                    if (!string.IsNullOrEmpty(Request.Form["User"]))
                        userId = Request.Form["User"];
                    else
                    {
                        userId = _userManager.GetUserId(User);
                    }
                    var user = _context.Users.Find(userId);
                    var item = _context.VendorItems.Find(itemId);
                    VendorItemGallery vendorItemGallery = new VendorItemGallery { Image = filename, MediaType = vendorItemGalleryViewModel.MediaType, Item = item };
                    _context.Add(vendorItemGallery);
                    await _context.SaveChangesAsync();
                }
                var parms = new Dictionary<string, string>
    {
        { "itemId", itemId.ToString() }
    };
                return RedirectToAction(nameof(Index), parms);
            
            }

            return View(vendorItemGalleryViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/VendorItemGalleries/Edit/{id?}")]
        // GET: VendorItemGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = _context.VendorItemGalleries.Include(x => x.Item).Where(x=>x.Id == id).FirstOrDefault();
            VendorItemGalleryViewModel model = new VendorItemGalleryViewModel { Id = vendorItemGallery.Id, MediaType = vendorItemGallery.MediaType };
            if (vendorItemGallery == null)
            {
                return NotFound();
            }
            ViewBag.ItemId = vendorItemGallery.Item.Id.ToString();
            return View(model);
        }

        // POST: VendorItemGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemGalleries/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] VendorItemGalleryViewModel vendorItemGalleryViewModel, int itemId)
        {
            if (ModelState.IsValid)
            {
                var parms = new Dictionary<string, string>
    {
        { "itemId", itemId.ToString() }
    };
                string userId = "";
                if (!string.IsNullOrEmpty(Request.Form["User"]))
                    userId = Request.Form["User"];
                else
                {
                    userId = _userManager.GetUserId(User);
                }
                var user = _context.Users.Find(userId);
                var item = _context.VendorItems.Find(itemId);
                var row = _context.VendorItemGalleries.Where(x => x.Id == id).FirstOrDefault();
                foreach (var img in vendorItemGalleryViewModel.Image)
                {
                    if (img != null)
                    {
                        string filename = Guid.NewGuid().ToString().Substring(4) + img.FileName;
                        UploadFile(img, filename);
                        row.MediaType = vendorItemGalleryViewModel.MediaType;
                        row.Image = filename;
                    }
                    else
                        row.Image = row.Image;
                    row.Item = item;
                    row.MediaType = vendorItemGalleryViewModel.MediaType;
                    await _context.SaveChangesAsync();
                }
                    return RedirectToAction(nameof(Index), parms);
            }
            return View(vendorItemGalleryViewModel);
        }
        [Route("Admin/VendorItemGalleries/Delete/{id?}")]
        // GET: VendorItemGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = await _context.VendorItemGalleries.Include(x=>x.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.ItemId = vendorItemGallery.Item.Id.ToString();
            if (vendorItemGallery == null)
            {
                return NotFound();
            }

            return View(vendorItemGallery);
        }

        // POST: VendorItemGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemGalleries/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItemGallery = _context.VendorItemGalleries.Include(x => x.Item).Where(x=>x.Id == id).FirstOrDefault();
            var parms = new Dictionary<string, string>
    {
        { "itemId", vendorItemGallery.Item.Id.ToString() }
    };
            _context.VendorItemGalleries.Remove(vendorItemGallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), parms);
        }

        private bool VendorItemGalleryExists(int id)
        {
            return _context.VendorItemGalleries.Any(e => e.Id == id);
        }
    }
}

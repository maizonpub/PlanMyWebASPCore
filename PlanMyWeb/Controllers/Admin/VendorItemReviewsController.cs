using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin,Vendor")]
    public class VendorItemReviewsController : Controller
    
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<Users> _userManager;
        public VendorItemReviewsController(DbWebContext context, IHostingEnvironment hostingEnvironment, UserManager<Users> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _userManager = userManager;
        }

        [Route("Admin/VendorItemReviews")]
        // GET: VendorItemReviews
        public async Task<IActionResult> Index(int itemId)
        {
            var vendorItemReviews = await _context.VendorItemReviews.ToListAsync();
            if (User.IsInRole("Vendor"))
            {
                var userId = _userManager.GetUserId(User);
                vendorItemReviews = vendorItemReviews.Where(x => x.VendorItemId == itemId).ToList();
            }
            return View(vendorItemReviews);

        }
        [Route("Admin/VendorItemReviews/Details/{id?}")]
        // GET: VendorItemReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemReviews = await _context.VendorItemReviews.Include(x => x.VendorItemId)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.ItemId = vendorItemReviews.VendorItemId.ToString();
            if (vendorItemReviews == null)
            {
                return NotFound();
            }

            return View(vendorItemReviews);
        }
        //private async void UploadFile(IFormFile media, string FileName)
        //{
        //    string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await media.CopyToAsync(stream);
        //    }
        //}

        [Route("Admin/VendorItemReviews/Edit/{id?}")]
        // GET: VendorItemReviews/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemReviews = _context.VendorItemReviews.Include(x => x.VendorItem).Where(x => x.Id == id).FirstOrDefault();
            
            if (vendorItemReviews == null)
            {
                return NotFound();
            }
            ViewBag.ItemId = vendorItemReviews.VendorItemId.ToString();
            return View(vendorItemReviews);
        }

        // POST: VendorItemReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemReviews/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,comment,author,email,subject,rating,DateIn,Status")] VendorItemReview vendorItemReviews, int itemId)
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
                _context.Update(vendorItemReviews);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), parms);
            }
            return View(vendorItemReviews);
        }
        
        [Route("Admin/VendorItemReviews/Delete/{id?}")]
        // GET: VendorItemReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemReviews = await _context.VendorItemReviews.Include(x => x.VendorItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.ItemId = vendorItemReviews.VendorItemId.ToString();
            if (vendorItemReviews == null)
            {
                return NotFound();
            }

            return View(vendorItemReviews);
        }

        // POST: VendorItemReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemReviews/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItemReviews = _context.VendorItemReviews.Include(x => x.VendorItem).Where(x => x.Id == id).FirstOrDefault();
            var parms = new Dictionary<string, string>
    {
        { "itemId", vendorItemReviews.VendorItemId.ToString() }
    };
            _context.VendorItemReviews.Remove(vendorItemReviews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), parms);
        }

        private bool VendorItemReviewExist(int id)
        {
            return _context.VendorItemReviews.Any(e => e.Id == id);
        }
    }
}
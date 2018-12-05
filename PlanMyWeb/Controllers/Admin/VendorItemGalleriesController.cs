using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Authorization;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class VendorItemGalleriesController : Controller
    {
        private readonly DbWebContext _context;

        public VendorItemGalleriesController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/VendorItemGalleries")]
        // GET: VendorItemGalleries
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorItemGalleries.ToListAsync());
        }
        [Route("Admin/VendorItemGalleries/Details/{id?}")]
        // GET: VendorItemGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = await _context.VendorItemGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemGallery == null)
            {
                return NotFound();
            }

            return View(vendorItemGallery);
        }
        [Route("Admin/VendorItemGalleries/Create")]
        // GET: VendorItemGalleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorItemGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemGalleries/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image")] VendorItemGallery vendorItemGallery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorItemGallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItemGallery);
        }
        [Route("Admin/VendorItemGalleries/Edit/{id?}")]
        // GET: VendorItemGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = await _context.VendorItemGalleries.FindAsync(id);
            if (vendorItemGallery == null)
            {
                return NotFound();
            }
            return View(vendorItemGallery);
        }

        // POST: VendorItemGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemGalleries/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] VendorItemGallery vendorItemGallery)
        {
            if (id != vendorItemGallery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorItemGallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorItemGalleryExists(vendorItemGallery.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItemGallery);
        }
        [Route("Admin/VendorItemGalleries/Delete/{id?}")]
        // GET: VendorItemGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = await _context.VendorItemGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var vendorItemGallery = await _context.VendorItemGalleries.FindAsync(id);
            _context.VendorItemGalleries.Remove(vendorItemGallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorItemGalleryExists(int id)
        {
            return _context.VendorItemGalleries.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace PlanMyWeb.Controllers.Admin
{
    public class VendorItemGalleriesController : Controller
    {
        private readonly DbWebContext _context;

        public VendorItemGalleriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: VendorItemGalleries
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorItemGalleries.ToListAsync());
        }

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

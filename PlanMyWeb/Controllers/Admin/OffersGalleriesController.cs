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
    
    public class OffersGalleriesController : Controller
    {
        private readonly DbWebContext _context;

        public OffersGalleriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: OffersGalleries
        public async Task<IActionResult> Index()
        {
            return View(await _context.OffersGalleries.ToListAsync());
        }

        // GET: OffersGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersGallery = await _context.OffersGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offersGallery == null)
            {
                return NotFound();
            }

            return View(offersGallery);
        }

        // GET: OffersGalleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OffersGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image")] OffersGallery offersGallery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offersGallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offersGallery);
        }

        // GET: OffersGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersGallery = await _context.OffersGalleries.FindAsync(id);
            if (offersGallery == null)
            {
                return NotFound();
            }
            return View(offersGallery);
        }

        // POST: OffersGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] OffersGallery offersGallery)
        {
            if (id != offersGallery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offersGallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffersGalleryExists(offersGallery.Id))
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
            return View(offersGallery);
        }

        // GET: OffersGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersGallery = await _context.OffersGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offersGallery == null)
            {
                return NotFound();
            }

            return View(offersGallery);
        }

        // POST: OffersGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offersGallery = await _context.OffersGalleries.FindAsync(id);
            _context.OffersGalleries.Remove(offersGallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffersGalleryExists(int id)
        {
            return _context.OffersGalleries.Any(e => e.Id == id);
        }
    }
}

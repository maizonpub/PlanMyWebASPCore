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
    
    public class OffersCategoriesController : Controller
    {
        private readonly DbWebContext _context;

        public OffersCategoriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: OffersCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.OffersCategories.ToListAsync());
        }

        // GET: OffersCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersCategory = await _context.OffersCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offersCategory == null)
            {
                return NotFound();
            }

            return View(offersCategory);
        }

        // GET: OffersCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OffersCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] OffersCategory offersCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offersCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offersCategory);
        }

        // GET: OffersCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersCategory = await _context.OffersCategories.FindAsync(id);
            if (offersCategory == null)
            {
                return NotFound();
            }
            return View(offersCategory);
        }

        // POST: OffersCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] OffersCategory offersCategory)
        {
            if (id != offersCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offersCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffersCategoryExists(offersCategory.Id))
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
            return View(offersCategory);
        }

        // GET: OffersCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersCategory = await _context.OffersCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offersCategory == null)
            {
                return NotFound();
            }

            return View(offersCategory);
        }

        // POST: OffersCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offersCategory = await _context.OffersCategories.FindAsync(id);
            _context.OffersCategories.Remove(offersCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffersCategoryExists(int id)
        {
            return _context.OffersCategories.Any(e => e.Id == id);
        }
    }
}

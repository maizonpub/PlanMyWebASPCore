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
    public class ReviewsController : Controller
    {
        private readonly DbWebContext _context;

        public ReviewsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorItemReviews.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemReview = await _context.VendorItemReviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemReview == null)
            {
                return NotFound();
            }

            return View(vendorItemReview);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,comment,author,email,subject,rating,DateIn,Status")] VendorItemReview vendorItemReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorItemReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItemReview);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemReview = await _context.VendorItemReviews.FindAsync(id);
            if (vendorItemReview == null)
            {
                return NotFound();
            }
            return View(vendorItemReview);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,comment,author,email,subject,rating,DateIn,Status")] VendorItemReview vendorItemReview)
        {
            if (id != vendorItemReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorItemReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorItemReviewExists(vendorItemReview.Id))
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
            return View(vendorItemReview);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemReview = await _context.VendorItemReviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemReview == null)
            {
                return NotFound();
            }

            return View(vendorItemReview);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var vendorItemReview = await _context.VendorItemReviews.FindAsync(id);
            _context.VendorItemReviews.Remove(vendorItemReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorItemReviewExists(long id)
        {
            return _context.VendorItemReviews.Any(e => e.Id == id);
        }
    }
}

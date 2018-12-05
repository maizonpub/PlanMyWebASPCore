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
    
    public class VendorCategoriesController : Controller
    {
        private readonly DbWebContext _context;

        public VendorCategoriesController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/VendorCategories")]
        // GET: VendorCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorCategories.ToListAsync());
        }
        [Route("Admin/VendorCategories/Details/{id?}")]
        // GET: VendorCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCategory = await _context.VendorCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorCategory == null)
            {
                return NotFound();
            }

            return View(vendorCategory);
        }
        [Route("Admin/VendorCategories/Create")]
        // GET: VendorCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorCategories/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image,Title")] VendorCategory vendorCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorCategory);
        }
        [Route("Admin/VendorCategories/Edit/{id?}")]
        // GET: VendorCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCategory = await _context.VendorCategories.FindAsync(id);
            if (vendorCategory == null)
            {
                return NotFound();
            }
            return View(vendorCategory);
        }

        // POST: VendorCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorCategories/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title")] VendorCategory vendorCategory)
        {
            if (id != vendorCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorCategoryExists(vendorCategory.Id))
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
            return View(vendorCategory);
        }
        [Route("Admin/VendorCategories/Delete/{id?}")]
        // GET: VendorCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCategory = await _context.VendorCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorCategory == null)
            {
                return NotFound();
            }

            return View(vendorCategory);
        }

        // POST: VendorCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorCategories/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorCategory = await _context.VendorCategories.FindAsync(id);
            _context.VendorCategories.Remove(vendorCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorCategoryExists(int id)
        {
            return _context.VendorCategories.Any(e => e.Id == id);
        }
    }
}

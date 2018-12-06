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
    public class VendorItemCategoriesController : Controller
    {
        private readonly DbWebContext _context;

        public VendorItemCategoriesController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/VendorItemCategories")]
        // GET: VendorItemCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorItemCategories.ToListAsync());
        }
        [Route("Admin/VendorItemCategories/Details/{id?}")]
        // GET: VendorItemCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemCategory = await _context.VendorItemCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemCategory == null)
            {
                return NotFound();
            }

            return View(vendorItemCategory);
        }
        [Route("Admin/VendorItemCategories/Create")]
        // GET: VendorItemCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorItemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemCategories/Create")]
        public async Task<IActionResult> Create([Bind("Id")] VendorItemCategory vendorItemCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorItemCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItemCategory);
        }
        [Route("Admin/VendorItemCategories/Edit/{id?}")]
        // GET: VendorItemCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemCategory = await _context.VendorItemCategories.FindAsync(id);
            if (vendorItemCategory == null)
            {
                return NotFound();
            }
            return View(vendorItemCategory);
        }

        // POST: VendorItemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemCategories/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] VendorItemCategory vendorItemCategory)
        {
            if (id != vendorItemCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorItemCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorItemCategoryExists(vendorItemCategory.Id))
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
            return View(vendorItemCategory);
        }
        [Route("Admin/VendorItemCategories/Delete/{id?}")]
        // GET: VendorItemCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemCategory = await _context.VendorItemCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemCategory == null)
            {
                return NotFound();
            }

            return View(vendorItemCategory);
        }

        // POST: VendorItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemCategories/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItemCategory = await _context.VendorItemCategories.FindAsync(id);
            _context.VendorItemCategories.Remove(vendorItemCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorItemCategoryExists(int id)
        {
            return _context.VendorItemCategories.Any(e => e.Id == id);
        }
    }
}

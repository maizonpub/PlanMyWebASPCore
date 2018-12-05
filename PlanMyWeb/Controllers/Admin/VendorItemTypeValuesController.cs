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
    
    public class VendorItemTypeValuesController : Controller
    {
        private readonly DbWebContext _context;

        public VendorItemTypeValuesController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/VendorItemTypeValues")]
        // GET: VendorItemTypeValues
        public async Task<IActionResult> Index()
        {
            var dbWebContext = _context.VendorItemTypeValues.Include(v => v.VendorItem);
            return View(await dbWebContext.ToListAsync());
        }
        [Route("Admin/VendorItemTypeValues/Details/{id?}")]
        // GET: VendorItemTypeValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemTypeValue = await _context.VendorItemTypeValues
                .Include(v => v.VendorItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemTypeValue == null)
            {
                return NotFound();
            }

            return View(vendorItemTypeValue);
        }
        [Route("Admin/VendorItemTypeValues/Create")]
        // GET: VendorItemTypeValues/Create
        public IActionResult Create()
        {
            ViewData["VendorItemId"] = new SelectList(_context.VendorItems, "Id", "Id");
            return View();
        }

        // POST: VendorItemTypeValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemTypeValues/Create")]
        public async Task<IActionResult> Create([Bind("Id,VendorItemId")] VendorItemTypeValue vendorItemTypeValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorItemTypeValue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorItemId"] = new SelectList(_context.VendorItems, "Id", "Id", vendorItemTypeValue.VendorItemId);
            return View(vendorItemTypeValue);
        }
        [Route("Admin/VendorItemTypeValues/Edit/{id?}")]
        // GET: VendorItemTypeValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemTypeValue = await _context.VendorItemTypeValues.FindAsync(id);
            if (vendorItemTypeValue == null)
            {
                return NotFound();
            }
            ViewData["VendorItemId"] = new SelectList(_context.VendorItems, "Id", "Id", vendorItemTypeValue.VendorItemId);
            return View(vendorItemTypeValue);
        }

        // POST: VendorItemTypeValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemTypeValues/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VendorItemId")] VendorItemTypeValue vendorItemTypeValue)
        {
            if (id != vendorItemTypeValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorItemTypeValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorItemTypeValueExists(vendorItemTypeValue.Id))
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
            ViewData["VendorItemId"] = new SelectList(_context.VendorItems, "Id", "Id", vendorItemTypeValue.VendorItemId);
            return View(vendorItemTypeValue);
        }
        [Route("Admin/VendorItemTypeValues/Delete/{id?}")]
        // GET: VendorItemTypeValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemTypeValue = await _context.VendorItemTypeValues
                .Include(v => v.VendorItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemTypeValue == null)
            {
                return NotFound();
            }

            return View(vendorItemTypeValue);
        }

        // POST: VendorItemTypeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemTypeValues/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItemTypeValue = await _context.VendorItemTypeValues.FindAsync(id);
            _context.VendorItemTypeValues.Remove(vendorItemTypeValue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorItemTypeValueExists(int id)
        {
            return _context.VendorItemTypeValues.Any(e => e.Id == id);
        }
    }
}

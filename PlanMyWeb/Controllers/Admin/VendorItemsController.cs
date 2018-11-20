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
    public class VendorItemsController : Controller
    {
        private readonly DbWebContext _context;

        public VendorItemsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: VendorItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorItems.ToListAsync());
        }

        // GET: VendorItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            return View(vendorItem);
        }

        // GET: VendorItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Latitude,Longitude,PhoneNumber,UserId,HtmlDescription,Address,Email")] VendorItem vendorItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItem);
        }

        // GET: VendorItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems.FindAsync(id);
            if (vendorItem == null)
            {
                return NotFound();
            }
            return View(vendorItem);
        }

        // POST: VendorItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Latitude,Longitude,PhoneNumber,UserId,HtmlDescription,Address,Email")] VendorItem vendorItem)
        {
            if (id != vendorItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorItemExists(vendorItem.Id))
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
            return View(vendorItem);
        }

        // GET: VendorItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            return View(vendorItem);
        }

        // POST: VendorItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItem = await _context.VendorItems.FindAsync(id);
            _context.VendorItems.Remove(vendorItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorItemExists(int id)
        {
            return _context.VendorItems.Any(e => e.Id == id);
        }
    }
}

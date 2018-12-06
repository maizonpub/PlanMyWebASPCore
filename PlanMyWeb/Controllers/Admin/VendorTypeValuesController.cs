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
    public class VendorTypeValuesController : Controller
    {
        private readonly DbWebContext _context;

        public VendorTypeValuesController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/VendorTypeValues")]
        // GET: VendorTypeValues
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorTypeValues.ToListAsync());
        }
        [Route("Admin/VendorTypeValues/Details/{id?}")]
        // GET: VendorTypeValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorTypeValue = await _context.VendorTypeValues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorTypeValue == null)
            {
                return NotFound();
            }

            return View(vendorTypeValue);
        }
        [Route("Admin/VendorTypeValues/Create")]
        // GET: VendorTypeValues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorTypeValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypeValues/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title")] VendorTypeValue vendorTypeValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorTypeValue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorTypeValue);
        }
        [Route("Admin/VendorTypeValues/Edit/{id?}")]
        // GET: VendorTypeValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorTypeValue = await _context.VendorTypeValues.FindAsync(id);
            if (vendorTypeValue == null)
            {
                return NotFound();
            }
            return View(vendorTypeValue);
        }

        // POST: VendorTypeValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypeValues/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] VendorTypeValue vendorTypeValue)
        {
            if (id != vendorTypeValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorTypeValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorTypeValueExists(vendorTypeValue.Id))
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
            return View(vendorTypeValue);
        }
        [Route("Admin/VendorTypeValues/Delete/{id?}")]
        // GET: VendorTypeValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorTypeValue = await _context.VendorTypeValues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorTypeValue == null)
            {
                return NotFound();
            }

            return View(vendorTypeValue);
        }

        // POST: VendorTypeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypeValues/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorTypeValue = await _context.VendorTypeValues.FindAsync(id);
            _context.VendorTypeValues.Remove(vendorTypeValue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorTypeValueExists(int id)
        {
            return _context.VendorTypeValues.Any(e => e.Id == id);
        }
    }
}

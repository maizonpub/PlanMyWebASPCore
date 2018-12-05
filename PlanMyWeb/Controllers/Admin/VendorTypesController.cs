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
    
    public class VendorTypesController : Controller
    {
        private readonly DbWebContext _context;

        public VendorTypesController(DbWebContext context)
        {
            _context = context;
        }

        [Route("Admin/VendorTypes")]
        // GET: VendorTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorTypes.ToListAsync());
        }
        [Route("Admin/VendorTypes/Details/{id?}")]
        // GET: VendorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorType == null)
            {
                return NotFound();
            }

            return View(vendorType);
        }
        [Route("Admin/VendorTypes/Create")]
        // GET: VendorTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypes/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title,CategoryId,VendorCategory")] VendorType vendorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorType);
        }
        [Route("Admin/VendorTypes/Edit/{id?}")]
        // GET: VendorTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorTypes.FindAsync(id);
            if (vendorType == null)
            {
                return NotFound();
            }
            return View(vendorType);
        }

        // POST: VendorTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypes/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CategoryId,VendorCategory")] VendorType vendorType)
        {
            if (id != vendorType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorTypeExists(vendorType.Id))
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
            return View(vendorType);
        }
        [Route("Admin/VendorTypes/Delete/{id?}")]
        // GET: VendorTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorType == null)
            {
                return NotFound();
            }

            return View(vendorType);
        }

        // POST: VendorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypes/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorType = await _context.VendorTypes.FindAsync(id);
            _context.VendorTypes.Remove(vendorType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorTypeExists(int id)
        {
            return _context.VendorTypes.Any(e => e.Id == id);
        }
    }
}

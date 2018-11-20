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
    public class GuestListTablesController : Controller
    {
        private readonly DbWebContext _context;

        public GuestListTablesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: GuestListTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.GuestListTables.ToListAsync());
        }

        // GET: GuestListTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestListTables = await _context.GuestListTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guestListTables == null)
            {
                return NotFound();
            }

            return View(guestListTables);
        }

        // GET: GuestListTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestListTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] GuestListTables guestListTables)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestListTables);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guestListTables);
        }

        // GET: GuestListTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestListTables = await _context.GuestListTables.FindAsync(id);
            if (guestListTables == null)
            {
                return NotFound();
            }
            return View(guestListTables);
        }

        // POST: GuestListTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] GuestListTables guestListTables)
        {
            if (id != guestListTables.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestListTables);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestListTablesExists(guestListTables.Id))
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
            return View(guestListTables);
        }

        // GET: GuestListTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestListTables = await _context.GuestListTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guestListTables == null)
            {
                return NotFound();
            }

            return View(guestListTables);
        }

        // POST: GuestListTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guestListTables = await _context.GuestListTables.FindAsync(id);
            _context.GuestListTables.Remove(guestListTables);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestListTablesExists(int id)
        {
            return _context.GuestListTables.Any(e => e.Id == id);
        }
    }
}

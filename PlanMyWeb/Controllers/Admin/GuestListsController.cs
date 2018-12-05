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
    
    public class GuestListsController : Controller
    {
        private readonly DbWebContext _context;

        public GuestListsController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/GuestLists")]
        // GET: GuestLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.GuestLists.ToListAsync());
        }
        [Route("Admin/GuestLists/Details/{id?}")]
        // GET: GuestLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestList = await _context.GuestLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guestList == null)
            {
                return NotFound();
            }

            return View(guestList);
        }
        [Route("Admin/GuestLists/Create")]
        // GET: GuestLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/GuestLists/Create")]
        public async Task<IActionResult> Create([Bind("Id,FullName,GuestStatus,Side,Email,Phone,Address")] GuestList guestList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guestList);
        }
        [Route("Admin/GuestLists/Edit/{id?}")]
        // GET: GuestLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestList = await _context.GuestLists.FindAsync(id);
            if (guestList == null)
            {
                return NotFound();
            }
            return View(guestList);
        }

        // POST: GuestLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/GuestLists/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,GuestStatus,Side,Email,Phone,Address")] GuestList guestList)
        {
            if (id != guestList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestListExists(guestList.Id))
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
            return View(guestList);
        }
        [Route("Admin/GuestLists/Delete/{id?}")]
        // GET: GuestLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestList = await _context.GuestLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guestList == null)
            {
                return NotFound();
            }

            return View(guestList);
        }

        // POST: GuestLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/GuestLists/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guestList = await _context.GuestLists.FindAsync(id);
            _context.GuestLists.Remove(guestList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestListExists(int id)
        {
            return _context.GuestLists.Any(e => e.Id == id);
        }
    }
}

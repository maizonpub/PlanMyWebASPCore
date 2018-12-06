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
    public class HomeTipsController : Controller
    {
        private readonly DbWebContext _context;

        public HomeTipsController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/HomeTips")]
        // GET: HomeTips
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeTips.ToListAsync());
        }
        [Route("Admin/HomeTips/Details/{id?}")]
        // GET: HomeTips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeTips = await _context.HomeTips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeTips == null)
            {
                return NotFound();
            }

            return View(homeTips);
        }
        [Route("Admin/HomeTips/Create")]
        // GET: HomeTips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeTips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeTips/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image,Title,Description")] HomeTips homeTips)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeTips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeTips);
        }
        [Route("Admin/HomeTips/Edit/{id?}")]
        // GET: HomeTips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeTips = await _context.HomeTips.FindAsync(id);
            if (homeTips == null)
            {
                return NotFound();
            }
            return View(homeTips);
        }

        // POST: HomeTips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeTips/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title,Description")] HomeTips homeTips)
        {
            if (id != homeTips.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeTips);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeTipsExists(homeTips.Id))
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
            return View(homeTips);
        }
        [Route("Admin/HomeTips/Delete/{id?}")]
        // GET: HomeTips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeTips = await _context.HomeTips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeTips == null)
            {
                return NotFound();
            }

            return View(homeTips);
        }

        // POST: HomeTips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeTips/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeTips = await _context.HomeTips.FindAsync(id);
            _context.HomeTips.Remove(homeTips);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeTipsExists(int id)
        {
            return _context.HomeTips.Any(e => e.Id == id);
        }
    }
}

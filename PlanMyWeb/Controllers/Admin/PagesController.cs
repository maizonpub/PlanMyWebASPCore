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
    public class PagesController : Controller
    {
        private readonly DbWebContext _context;

        public PagesController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/UsefulLinks")]
        // GET: Pages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pages.ToListAsync());
        }
        [Route("Admin/UsefulLinks/Details/{id?}")]
        // GET: Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pages == null)
            {
                return NotFound();
            }

            return View(pages);
        }
        [Route("Admin/UsefulLinks/Create")]
        // GET: Pages/Create
        public IActionResult Create()
        {
            return View();
        }

        [Route("Admin/UsefulLinks/Create")]
        // POST: Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HtmlDescription")] Pages pages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pages);
        }

        [Route("Admin/UsefulLinks/Edit")]
        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages.FindAsync(id);
            if (pages == null)
            {
                return NotFound();
            }
            return View(pages);
        }

        [Route("Admin/UsefulLinks/Edit")]
        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HtmlDescription")] Pages pages)
        {
            if (id != pages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagesExists(pages.Id))
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
            return View(pages);
        }

        [Route("Admin/UsefulLinks/Delete")]
        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pages == null)
            {
                return NotFound();
            }

            return View(pages);
        }

        [Route("Admin/UsefulLinks/Delete")]
        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pages = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(pages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagesExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}

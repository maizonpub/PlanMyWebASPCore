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
    public class HomeSlidersController : Controller
    {
        private readonly DbWebContext _context;

        public HomeSlidersController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/HomeSliders")]
        // GET: HomeSliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeSlider.ToListAsync());
        }
        [Route("Admin/HomeSliders/Details/{id?}")]
        // GET: HomeSliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeSlider = await _context.HomeSlider
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeSlider == null)
            {
                return NotFound();
            }

            return View(homeSlider);
        }
        [Route("Admin/HomeSliders/Create")]
        // GET: HomeSliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeSliders/Create")]
        public async Task<IActionResult> Create([Bind("Id,Media,MediaType")] HomeSlider homeSlider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeSlider);
        }
        [Route("Admin/HomeSliders/Edit/{id?}")]
        // GET: HomeSliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeSlider = await _context.HomeSlider.FindAsync(id);
            if (homeSlider == null)
            {
                return NotFound();
            }
            return View(homeSlider);
        }

        // POST: HomeSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeSliders/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Media,MediaType")] HomeSlider homeSlider)
        {
            if (id != homeSlider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeSlider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeSliderExists(homeSlider.Id))
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
            return View(homeSlider);
        }
        [Route("Admin/HomeSliders/Delete/{id?}")]
        // GET: HomeSliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeSlider = await _context.HomeSlider
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeSlider == null)
            {
                return NotFound();
            }

            return View(homeSlider);
        }

        // POST: HomeSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeSliders/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeSlider = await _context.HomeSlider.FindAsync(id);
            _context.HomeSlider.Remove(homeSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeSliderExists(int id)
        {
            return _context.HomeSlider.Any(e => e.Id == id);
        }
    }
}

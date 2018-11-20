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
    public class BlogCategoryRelationsController : Controller
    {
        private readonly DbWebContext _context;

        public BlogCategoryRelationsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: BlogCategoryRelations
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogCategoryRelations.ToListAsync());
        }

        // GET: BlogCategoryRelations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategoryRelation = await _context.BlogCategoryRelations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategoryRelation == null)
            {
                return NotFound();
            }

            return View(blogCategoryRelation);
        }

        // GET: BlogCategoryRelations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogCategoryRelations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] BlogCategoryRelation blogCategoryRelation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogCategoryRelation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogCategoryRelation);
        }

        // GET: BlogCategoryRelations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategoryRelation = await _context.BlogCategoryRelations.FindAsync(id);
            if (blogCategoryRelation == null)
            {
                return NotFound();
            }
            return View(blogCategoryRelation);
        }

        // POST: BlogCategoryRelations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] BlogCategoryRelation blogCategoryRelation)
        {
            if (id != blogCategoryRelation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogCategoryRelation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogCategoryRelationExists(blogCategoryRelation.Id))
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
            return View(blogCategoryRelation);
        }

        // GET: BlogCategoryRelations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategoryRelation = await _context.BlogCategoryRelations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategoryRelation == null)
            {
                return NotFound();
            }

            return View(blogCategoryRelation);
        }

        // POST: BlogCategoryRelations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogCategoryRelation = await _context.BlogCategoryRelations.FindAsync(id);
            _context.BlogCategoryRelations.Remove(blogCategoryRelation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCategoryRelationExists(int id)
        {
            return _context.BlogCategoryRelations.Any(e => e.Id == id);
        }
    }
}

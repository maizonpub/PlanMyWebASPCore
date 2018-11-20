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
    public class BudgetCategoriesController : Controller
    {
        private readonly DbWebContext _context;

        public BudgetCategoriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: BudgetCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.BudgetCategories.ToListAsync());
        }

        // GET: BudgetCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetCategory = await _context.BudgetCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budgetCategory == null)
            {
                return NotFound();
            }

            return View(budgetCategory);
        }

        // GET: BudgetCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BudgetCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] BudgetCategory budgetCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budgetCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(budgetCategory);
        }

        // GET: BudgetCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetCategory = await _context.BudgetCategories.FindAsync(id);
            if (budgetCategory == null)
            {
                return NotFound();
            }
            return View(budgetCategory);
        }

        // POST: BudgetCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] BudgetCategory budgetCategory)
        {
            if (id != budgetCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgetCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetCategoryExists(budgetCategory.Id))
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
            return View(budgetCategory);
        }

        // GET: BudgetCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetCategory = await _context.BudgetCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budgetCategory == null)
            {
                return NotFound();
            }

            return View(budgetCategory);
        }

        // POST: BudgetCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budgetCategory = await _context.BudgetCategories.FindAsync(id);
            _context.BudgetCategories.Remove(budgetCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetCategoryExists(int id)
        {
            return _context.BudgetCategories.Any(e => e.Id == id);
        }
    }
}

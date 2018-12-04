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
   
    public class BasketItemsController : Controller
    {
        private readonly DbWebContext _context;

        public BasketItemsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: BasketItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.BasketItems.ToListAsync());
        }

        // GET: BasketItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketItems = await _context.BasketItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basketItems == null)
            {
                return NotFound();
            }

            return View(basketItems);
        }

        // GET: BasketItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BasketItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,TotalPrice")] BasketItems basketItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basketItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(basketItems);
        }

        // GET: BasketItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketItems = await _context.BasketItems.FindAsync(id);
            if (basketItems == null)
            {
                return NotFound();
            }
            return View(basketItems);
        }

        // POST: BasketItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,TotalPrice")] BasketItems basketItems)
        {
            if (id != basketItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basketItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketItemsExists(basketItems.Id))
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
            return View(basketItems);
        }

        // GET: BasketItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketItems = await _context.BasketItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basketItems == null)
            {
                return NotFound();
            }

            return View(basketItems);
        }

        // POST: BasketItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basketItems = await _context.BasketItems.FindAsync(id);
            _context.BasketItems.Remove(basketItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasketItemsExists(int id)
        {
            return _context.BasketItems.Any(e => e.Id == id);
        }
    }
}

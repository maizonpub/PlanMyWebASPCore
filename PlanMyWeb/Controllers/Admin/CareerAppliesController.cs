using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize (Roles = "Admin")]
    public class CareerAppliesController : Controller
    {
        private readonly DbWebContext _context;

        public CareerAppliesController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/CareerApplies")]
        // GET: CareerApplies
        public async Task<IActionResult> Index()
        {
            return View(await _context.CareerApplies.ToListAsync());
        }
        [Route("Admin/CareerApplies/Details/{id?}")]
        // GET: CareerApplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careerapplies = await _context.CareerApplies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (careerapplies == null)
            {
                return NotFound();
            }

            return View(careerapplies);
        }
        [Route("Admin/CareerApplies/Create")]
        // GET: CareerApplies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CareerApplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/CareerApplies/Create")]
        public async Task<IActionResult> Create([Bind("Id,Career,CV,Name,Phone,Email")] CareerApplies careerapplies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(careerapplies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(careerapplies);
        }
        [Route("Admin/CareerApplies/Edit/{id?}")]
        // GET: CareerApplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careerapplies = await _context.CareerApplies.FindAsync(id);
            if (careerapplies == null)
            {
                return NotFound();
            }
            return View(careerapplies);
        }

        // POST: CareerApplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/CareerApplies/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Career,CV,Name,Phone,Email")] CareerApplies careerapplies)
        {
            if (id != careerapplies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(careerapplies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareerAppliesExists(careerapplies.Id))
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
            return View(careerapplies);
        }

        private bool CareerAppliesExists(int id)
        {
            throw new NotImplementedException();
        }

        [Route("Admin/CareerApplies/Delete/{id?}")]
        // GET: CareerApplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careerapplies = await _context.CareerApplies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (careerapplies == null)
            {
                return NotFound();
            }

            return View(careerapplies);
        }

        // POST: CareerApplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/CareerApplies/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var careerapplies = await _context.CareerApplies.FindAsync(id);
            _context.CareerApplies.Remove(careerapplies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
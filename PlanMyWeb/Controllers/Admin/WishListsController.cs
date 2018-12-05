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
    public class WishListsController : Controller
    {
        private readonly DbWebContext _context;

        public WishListsController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/WishLists")]
        // GET: WishLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.WishLists.ToListAsync());
        }
        [Route("Admin/WishLists/Details/{id?}")]
        // GET: WishLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishList == null)
            {
                return NotFound();
            }

            return View(wishList);
        }
        [Route("Admin/WishLists/Create")]
        // GET: WishLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WishLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/WishLists/Create")]
        public async Task<IActionResult> Create([Bind("Id")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wishList);
        }
        [Route("Admin/WishLists/Edit/{id?}")]
        // GET: WishLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists.FindAsync(id);
            if (wishList == null)
            {
                return NotFound();
            }
            return View(wishList);
        }

        // POST: WishLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/WishLists/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] WishList wishList)
        {
            if (id != wishList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishListExists(wishList.Id))
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
            return View(wishList);
        }
        [Route("Admin/WishLists/Delete/{id?}")]
        // GET: WishLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishList == null)
            {
                return NotFound();
            }

            return View(wishList);
        }

        // POST: WishLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/WishLists/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wishList = await _context.WishLists.FindAsync(id);
            _context.WishLists.Remove(wishList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishListExists(int id)
        {
            return _context.WishLists.Any(e => e.Id == id);
        }
    }
}

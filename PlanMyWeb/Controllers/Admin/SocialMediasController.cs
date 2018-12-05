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
    public class SocialMediasController : Controller
    {
        private readonly DbWebContext _context;

        public SocialMediasController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/SocialMedias")]
        // GET: SocialMedias
        public async Task<IActionResult> Index()
        {
            return View(await _context.SocialMedias.ToListAsync());
        }
        [Route("Admin/SocialMedias/Details/{id?}")]
        // GET: SocialMedias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMedia = await _context.SocialMedias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialMedia == null)
            {
                return NotFound();
            }

            return View(socialMedia);
        }
        [Route("Admin/SocialMedias/Create")]
        // GET: SocialMedias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SocialMedias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/SocialMedias/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title,Link,Image")] SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socialMedia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socialMedia);
        }
        [Route("Admin/SocialMedias/Edit/{id?}")]
        // GET: SocialMedias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMedia = await _context.SocialMedias.FindAsync(id);
            if (socialMedia == null)
            {
                return NotFound();
            }
            return View(socialMedia);
        }

        // POST: SocialMedias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/SocialMedias/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Link,Image")] SocialMedia socialMedia)
        {
            if (id != socialMedia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socialMedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialMediaExists(socialMedia.Id))
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
            return View(socialMedia);
        }
        [Route("Admin/SocialMedias/Delete/{id?}")]
        // GET: SocialMedias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMedia = await _context.SocialMedias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialMedia == null)
            {
                return NotFound();
            }

            return View(socialMedia);
        }

        // POST: SocialMedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/SocialMedias/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var socialMedia = await _context.SocialMedias.FindAsync(id);
            _context.SocialMedias.Remove(socialMedia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialMediaExists(int id)
        {
            return _context.SocialMedias.Any(e => e.Id == id);
        }
    }
}

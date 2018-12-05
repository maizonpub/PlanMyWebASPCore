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
    
    public class WebContentsController : Controller
    {
        private readonly DbWebContext _context;

        public WebContentsController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/WebContents")]
        // GET: WebContents
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebContents.ToListAsync());
        }
        [Route("Admin/WebContents/Details/{id?}")]
        // GET: WebContents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webContent = await _context.WebContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webContent == null)
            {
                return NotFound();
            }

            return View(webContent);
        }
        [Route("Admin/WebContents/Create")]
        // GET: WebContents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebContents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/WebContents/Create")]
        public async Task<IActionResult> Create([Bind("Id,TipsTitle,FeaturedVendorsTitle,BlogTitle,ContactAddress,ContactEmail,ContactPhone,AdminEmail,AdminEmailPassword")] WebContent webContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webContent);
        }
        [Route("Admin/WebContents/Edit/{id?}")]
        // GET: WebContents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webContent = await _context.WebContents.FindAsync(id);
            if (webContent == null)
            {
                return NotFound();
            }
            return View(webContent);
        }

        // POST: WebContents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/WebContents/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipsTitle,FeaturedVendorsTitle,BlogTitle,ContactAddress,ContactEmail,ContactPhone,AdminEmail,AdminEmailPassword")] WebContent webContent)
        {
            if (id != webContent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebContentExists(webContent.Id))
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
            return View(webContent);
        }
        [Route("Admin/WebContents/Delete/{id?}")]
        // GET: WebContents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webContent = await _context.WebContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webContent == null)
            {
                return NotFound();
            }

            return View(webContent);
        }

        // POST: WebContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/WebContents/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webContent = await _context.WebContents.FindAsync(id);
            _context.WebContents.Remove(webContent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebContentExists(int id)
        {
            return _context.WebContents.Any(e => e.Id == id);
        }
    }
}

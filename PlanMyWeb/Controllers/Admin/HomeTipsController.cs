using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PlanMyWeb.Controllers
{
    public class HomeTipsController : Controller
    {
        private readonly DbWebContext _context;
        private IHostingEnvironment _hostingEnvironment;
        public HomeTipsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: HomeTips
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeTips.ToListAsync());
        }

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
        public async Task<IActionResult> Create([Bind("Id,Image,Title,Description")] HomeTipsViewModel homeTips)
        {
            if (ModelState.IsValid)
            {
                FormFile file = homeTips.Image as FormFile;
                string filepath = await Upload(file);
                HomeTips tip = new HomeTips { Description = homeTips.Description, Image = filepath, Title = homeTips.Title};
                _context.Add(homeTips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeTips);
        }
        public async Task<string> Upload(FormFile file)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Media");
            
                if (file.Length > 0)
                {
                string filename = Guid.NewGuid().ToString().Substring(0,5) + file.FileName;
                    var filePath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            
            return file.FileName;
        }
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

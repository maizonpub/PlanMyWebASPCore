using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class HomeTipsController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeTipsController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        
        [Route("Admin/HomeTips")]
        // GET: HomeTips
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeTips.ToListAsync());
        }
        [Route("Admin/HomeTips/Details/{id?}")]
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
        [Route("Admin/HomeTips/Create")]
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
        [Route("Admin/HomeTips/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image,Title,Description")] homeTipsViewModel homeTipsViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString().Substring(4) + homeTipsViewModel.Image.FileName;
                UploadFile(homeTipsViewModel.Image, filename);
                HomeTips homeTips = new HomeTips { Media = filename, MediaType = homeTipsViewModel.MediaType, Description = homeTipsViewModel.Description, Title = homeTipsViewModel.Title, Image = filename };
                _context.Add(homeTips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeTipsViewModel);
        }

        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/HomeTips/Edit/{id?}")]
        // GET: HomeTips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeTips = await _context.HomeTips.FindAsync(id);
            homeTipsViewModel model = new homeTipsViewModel { Id = homeTips.Id, MediaType = homeTips.MediaType, Title = homeTips.Title, Description = homeTips.Description };
            if (homeTips == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: HomeTips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeTips/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title,Description")] homeTipsViewModel homeTipsViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.HomeTips.Where(x => x.Id == id).FirstOrDefault();
                if (homeTipsViewModel.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + homeTipsViewModel.Image.FileName;
                    UploadFile(homeTipsViewModel.Image, filename);
                    row.MediaType = homeTipsViewModel.MediaType;
                }
                else
                    row.Media = row.Media;
                row.Title = homeTipsViewModel.Title;
                row.Description = homeTipsViewModel.Description;
                row.MediaType = homeTipsViewModel.MediaType;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeTipsViewModel);
        }
        [Route("Admin/HomeTips/Delete/{id?}")]
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
        [Route("Admin/HomeTips/Delete/{id?}")]
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

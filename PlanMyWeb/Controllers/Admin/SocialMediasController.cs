using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class SocialMediasController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public SocialMediasController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> Create([Bind("Id,Title,Link,Image")] SocialMediaViewModel socialMediaViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString().Substring(4) + socialMediaViewModel.Image.FileName;
                UploadFile(socialMediaViewModel.Image, filename);
                HomeSlider homeSlider = new HomeSlider { Media = filename, MediaType = socialMediaViewModel.MediaType };
                _context.Add(homeSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socialMediaViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
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
            SocialMediaViewModel model = new SocialMediaViewModel { Id = socialMedia.Id, MediaType = socialMedia.MediaType};
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Link,Image")] SocialMediaViewModel socialMediaViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.BlogCategories.Where(x => x.Id == id).FirstOrDefault();
                if (socialMediaViewModel.Image != null)
                {

                    string filename = Guid.NewGuid().ToString().Substring(4) + socialMediaViewModel.Image.FileName;
                    UploadFile(socialMediaViewModel.Image, filename);
                    row.MediaType = socialMediaViewModel.MediaType;
                }
                else
                row.Media = row.Media;
                row.MediaType = socialMediaViewModel.MediaType;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socialMediaViewModel);
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

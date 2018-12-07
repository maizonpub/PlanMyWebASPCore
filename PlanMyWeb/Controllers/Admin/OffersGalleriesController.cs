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
    public class OffersGalleriesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public OffersGalleriesController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/OffersGalleries")]
        // GET: OffersGalleries
        public async Task<IActionResult> Index()
        {
            return View(await _context.OffersGalleries.ToListAsync());
        }
        [Route("Admin/OffersGalleries/Details/{id?}")]
        // GET: OffersGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersGallery = await _context.OffersGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offersGallery == null)
            {
                return NotFound();
            }

            return View(offersGallery);
        }
        [Route("Admin/OffersGalleries/Create")]
        // GET: OffersGalleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OffersGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/OffersGalleries/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image")] OffersGalleryViewModel offersGalleryViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString().Substring(4) + offersGalleryViewModel.Image.FileName;
                UploadFile(offersGalleryViewModel.Image, filename);
                HomeSlider homeSlider = new HomeSlider { Media = filename, MediaType = offersGalleryViewModel.MediaType };
                _context.Add(homeSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offersGalleryViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/OffersGalleries/Edit/{id?}")]
        // GET: OffersGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersGallery = await _context.OffersGalleries.FindAsync(id);
            OffersGalleryViewModel model = new OffersGalleryViewModel { Id = offersGallery.Id, MediaType = offersGallery.MediaType};
            if (offersGallery == null)
            {
                return NotFound();
            }
            return View(offersGallery);
        }

        // POST: OffersGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/OffersGalleries/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] OffersGalleryViewModel offersGalleryViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.BlogCategories.Where(x => x.Id == id).FirstOrDefault();
                if (offersGalleryViewModel.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + offersGalleryViewModel.Image.FileName;
                    UploadFile(offersGalleryViewModel.Image, filename);
                    row.MediaType = offersGalleryViewModel.MediaType;
                }
                else
                    row.Media = row.Media;
                row.MediaType = offersGalleryViewModel.MediaType;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(offersGalleryViewModel);
        }
        [Route("Admin/OffersGalleries/Delete/{id?}")]
        // GET: OffersGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offersGallery = await _context.OffersGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offersGallery == null)
            {
                return NotFound();
            }

            return View(offersGallery);
        }

        // POST: OffersGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/OffersGalleries/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offersGallery = await _context.OffersGalleries.FindAsync(id);
            _context.OffersGalleries.Remove(offersGallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffersGalleryExists(int id)
        {
            return _context.OffersGalleries.Any(e => e.Id == id);
        }
    }
}

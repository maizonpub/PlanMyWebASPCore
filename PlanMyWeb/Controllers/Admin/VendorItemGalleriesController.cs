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
    public class VendorItemGalleriesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public VendorItemGalleriesController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/VendorItemGalleries")]
        // GET: VendorItemGalleries
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorItemGalleries.ToListAsync());
        }
        [Route("Admin/VendorItemGalleries/Details/{id?}")]
        // GET: VendorItemGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = await _context.VendorItemGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemGallery == null)
            {
                return NotFound();
            }

            return View(vendorItemGallery);
        }
        [Route("Admin/VendorItemGalleries/Create")]
        // GET: VendorItemGalleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorItemGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemGalleries/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image")] VendorItemGalleryViewModel vendorItemGalleryViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString().Substring(4) + vendorItemGalleryViewModel.Image.FileName;
                UploadFile(vendorItemGalleryViewModel.Image, filename);
                HomeSlider homeSlider = new HomeSlider { Media = filename, MediaType = vendorItemGalleryViewModel.MediaType };
                _context.Add(homeSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vendorItemGalleryViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/VendorItemGalleries/Edit/{id?}")]
        // GET: VendorItemGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = await _context.VendorItemGalleries.FindAsync(id);
            VendorItemGalleryViewModel model = new VendorItemGalleryViewModel { Id = vendorItemGallery.Id, MediaType = vendorItemGallery.MediaType };
            if (vendorItemGallery == null)
            {
                return NotFound();
            }
            return View(vendorItemGallery);
        }

        // POST: VendorItemGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemGalleries/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] VendorItemGalleryViewModel vendorItemGalleryViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.BlogCategories.Where(x => x.Id == id).FirstOrDefault();
                if (vendorItemGalleryViewModel.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + vendorItemGalleryViewModel.Image.FileName;
                    UploadFile(vendorItemGalleryViewModel.Image, filename);
                    row.MediaType = vendorItemGalleryViewModel.MediaType;
                }
                else
                    row.Media = row.Media;
                row.MediaType = vendorItemGalleryViewModel.MediaType;
                await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
            return View(vendorItemGalleryViewModel);
        }
        [Route("Admin/VendorItemGalleries/Delete/{id?}")]
        // GET: VendorItemGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItemGallery = await _context.VendorItemGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItemGallery == null)
            {
                return NotFound();
            }

            return View(vendorItemGallery);
        }

        // POST: VendorItemGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItemGalleries/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItemGallery = await _context.VendorItemGalleries.FindAsync(id);
            _context.VendorItemGalleries.Remove(vendorItemGallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorItemGalleryExists(int id)
        {
            return _context.VendorItemGalleries.Any(e => e.Id == id);
        }
    }
}

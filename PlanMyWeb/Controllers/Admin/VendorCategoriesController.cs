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
    public class VendorCategoriesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public VendorCategoriesController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/VendorCategories")]
        // GET: VendorCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorCategories.ToListAsync());
        }
        [Route("Admin/VendorCategories/Details/{id?}")]
        // GET: VendorCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCategory = await _context.VendorCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorCategory == null)
            {
                return NotFound();
            }

            return View(vendorCategory);
        }
        [Route("Admin/VendorCategories/Create")]
        // GET: VendorCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorCategories/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image,Title")] VendorCategoryViewModel vendorCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (vendorCategoryViewModel.Image != null)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + vendorCategoryViewModel.Image.FileName;
                    UploadFile(vendorCategoryViewModel.Image, filename);
                }
                VendorCategory vendorCategory = new VendorCategory { Image = filename, MediaType = vendorCategoryViewModel.MediaType , Title = vendorCategoryViewModel.Title};
                _context.Add(vendorCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vendorCategoryViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/VendorCategories/Edit/{id?}")]
        // GET: VendorCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCategory = await _context.VendorCategories.FindAsync(id);
            VendorCategoryViewModel model = new VendorCategoryViewModel { Id = vendorCategory.Id, MediaType = vendorCategory.MediaType, Title= vendorCategory.Title };
            if (vendorCategory == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: VendorCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorCategories/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title")] VendorCategoryViewModel vendorCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.VendorCategories.Where(x => x.Id == id).FirstOrDefault();
                if (vendorCategoryViewModel.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + vendorCategoryViewModel.Image.FileName;
                    UploadFile(vendorCategoryViewModel.Image, filename);
                    row.MediaType = vendorCategoryViewModel.MediaType;
                    row.Image = filename;
                }
                else
                    row.Image = row.Image;
                row.Title = vendorCategoryViewModel.Title;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                return View(vendorCategoryViewModel);
        }
        [Route("Admin/VendorCategories/Delete/{id?}")]
        // GET: VendorCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCategory = await _context.VendorCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorCategory == null)
            {
                return NotFound();
            }

            return View(vendorCategory);
        }

        // POST: VendorCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorCategories/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorCategory = await _context.VendorCategories.FindAsync(id);
            _context.VendorCategories.Remove(vendorCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorCategoryExists(int id)
        {
            return _context.VendorCategories.Any(e => e.Id == id);
        }
    }
}

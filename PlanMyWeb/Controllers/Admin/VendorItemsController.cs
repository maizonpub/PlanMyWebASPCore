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
    public class VendorItemsViewModel : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public VendorItemsViewModel(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/VendorItems")]
        // GET: VendorItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorItems.ToListAsync());
        }
        [Route("Admin/VendorItems/Details/{id?}")]
        // GET: VendorItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            return View(vendorItem);
        }
        [Route("Admin/VendorItems/Create")]
        // GET: VendorItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItems/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title,Latitude,Longitude,Location,PhoneNumber,HtmlDescription,Address,Email,IsFeatured,Thumb")] VendorItemViewModel vendorItemViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString().Substring(4) + vendorItemViewModel.Thumb.FileName;
                UploadFile(vendorItemViewModel.Thumb, filename);
                HomeSlider homeSlider = new HomeSlider { Media = filename, MediaType = vendorItemViewModel.MediaType };
                _context.Add(homeSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItemViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        [Route("Admin/VendorItems/Edit/{id?}")]
        // GET: VendorItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems.FindAsync(id);
            VendorItemViewModel model = new VendorItemViewModel { Id = vendorItem.Id, MediaType = vendorItem.MediaType };
            if (vendorItem == null)
            {
                return NotFound();
            }
            return View(vendorItem);
        }

        // POST: VendorItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItems/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Latitude,Longitude,Location,PhoneNumber,HtmlDescription,Address,Email,IsFeatured,Thumb")] VendorItemViewModel vendorItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.VendorItems.Where(x => x.Id == id).FirstOrDefault();
                if (vendorItemViewModel.Thumb != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + vendorItemViewModel.Thumb.FileName;
                    UploadFile(vendorItemViewModel.Thumb, filename);
                    row.MediaType = vendorItemViewModel.MediaType;
                }
                else
                    row.Thumb = row.Thumb;
                row.MediaType = vendorItemViewModel.MediaType;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorItemViewModel);
        }
           
        
        [Route("Admin/VendorItems/Delete/{id?}")]
        // GET: VendorItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorItem = await _context.VendorItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            return View(vendorItem);
        }

        // POST: VendorItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorItems/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItem = await _context.VendorItems.FindAsync(id);
            _context.VendorItems.Remove(vendorItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorItemExists(int id)
        {
            return _context.VendorItems.Any(e => e.Id == id);
        }
        
    }
}

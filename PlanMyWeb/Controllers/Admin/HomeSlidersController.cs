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
    public class HomeSlidersController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeSlidersController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [Route("Admin/HomeSliders")]
        // GET: HomeSliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeSlider.ToListAsync());
        }
        [Route("Admin/HomeSliders/Details/{id?}")]
        // GET: HomeSliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeSlider = await _context.HomeSlider
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeSlider == null)
            {
                return NotFound();
            }

            return View(homeSlider);
        }
        [Route("Admin/HomeSliders/Create")]
        // GET: HomeSliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeSliders/Create")]
        public async Task<IActionResult> Create([Bind("Id,Media,MediaType")] HomeSliderViewModel homeSliderViewModel)
        {
            
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString().Substring(4) + homeSliderViewModel.Media.FileName;
                UploadFile(homeSliderViewModel.Media, filename);
                HomeSlider homeSlider = new HomeSlider { Media = filename, MediaType = homeSliderViewModel.MediaType };
                _context.Add(homeSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeSliderViewModel);
        }

        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/HomeSliders/Edit/{id?}")]
        // GET: HomeSliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeSlider = await _context.HomeSlider.FindAsync(id);
            if (homeSlider == null)
            {
                return NotFound();
            }
            return View(homeSlider);
        }


        // POST: HomeSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeSliders/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Media,MediaType")] HomeSliderViewModel homeSliderViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString().Substring(4) + homeSliderViewModel.Media.FileName;
                UploadFile(homeSliderViewModel.Media, filename);
                var row = _context.HomeSlider.Where(x => x.Id == id).FirstOrDefault();
                row.Media = filename;
                row.MediaType = homeSliderViewModel.MediaType;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            return View(homeSliderViewModel);
        }
        

        [Route("Admin/HomeSliders/Delete/{id?}")]
        // GET: HomeSliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeSlider = await _context.HomeSlider
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeSlider == null)
            {
                return NotFound();
            }

            return View(homeSlider);
        }

        // POST: HomeSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/HomeSliders/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeSlider = await _context.HomeSlider.FindAsync(id);
            _context.HomeSlider.Remove(homeSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeSliderExists(int id)
        {
            return _context.HomeSlider.Any(e => e.Id == id);
        }
    }
}

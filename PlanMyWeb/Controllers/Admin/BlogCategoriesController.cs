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
    public class BlogCategoriesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BlogCategoriesController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/BlogCategories")]
        // GET: BlogCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogCategories.ToListAsync());
        }
        [Route("Admin/BlogCategories/Details/{id?}")]
        // GET: BlogCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }
        [Route("Admin/BlogCategories/Create")]
        // GET: BlogCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/BlogCategories/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image,Title")] BlogCategoryViewModel blogCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (blogCategoryViewModel.Image!=null)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + blogCategoryViewModel.Image.FileName;
                    UploadFile(blogCategoryViewModel.Image, filename);
                }
                BlogCategory category = new BlogCategory { Image = filename, Title = blogCategoryViewModel.Title };
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogCategoryViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/BlogCategories/Edit/{id?}")]
        // GET: BlogCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories.FindAsync(id);
            BlogCategoryViewModel model = new BlogCategoryViewModel { Id = blogCategory.Id, MediaType = blogCategory.MediaType, Title = blogCategory.Title };
            if (blogCategory == null)
            {
                return NotFound();
            }
            return View(blogCategory);
        }

        // POST: BlogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/BlogCategories/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title")] BlogCategoryViewModel blogCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.BlogCategories.Where(x => x.Id == id).FirstOrDefault();
                if (blogCategoryViewModel.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + blogCategoryViewModel.Image.FileName;
                    UploadFile(blogCategoryViewModel.Image, filename);
                    row.Image = filename;
                }
                else
                row.Image = row.Image;
                row.Title = blogCategoryViewModel.Title;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(blogCategoryViewModel);
        }
        [Route("Admin/BlogCategories/Delete/{id?}")]
        // GET: BlogCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // POST: BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/BlogCategories/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogCategory = await _context.BlogCategories.FindAsync(id);
            _context.BlogCategories.Remove(blogCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCategoryExists(int id)
        {
            return _context.BlogCategories.Any(e => e.Id == id);
        }
    }
}

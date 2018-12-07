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
    public class BlogsController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BlogsController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/Blogs")]
        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }
        [Route("Admin/Blog/Details/{id?}")]
        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }
        [Route("Admin/Blogs/Create")]
        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Blogs/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title,Image,HtmlDescription,PostDate")] BlogViewModel blogViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString().Substring(4) + blogViewModel.Image.FileName;
                UploadFile(blogViewModel.Image, filename);
                HomeSlider homeSlider = new HomeSlider { Media = filename, MediaType = blogViewModel.MediaType };
                _context.Add(homeSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogViewModel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        [Route("Admin/Blogs/Edit/{id?}")]
        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            BlogViewModel model = new BlogViewModel { Id = blog.Id, MediaType = blog.MediaType };
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Blogs/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Image,HtmlDescription,PostDate")] BlogViewModel blogViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.BlogCategories.Where(x => x.Id == id).FirstOrDefault();
                if (blogViewModel.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + blogViewModel.Image.FileName;
                    UploadFile(blogViewModel.Image, filename);
                    row.MediaType = blogViewModel.MediaType;
                }
                else
                    row.Media = row.Media;
                    row.MediaType = blogViewModel.MediaType;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(blogViewModel);
        }
        
        [Route("Admin/Blogs/Delete/{id?}")]
        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Blogs/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}

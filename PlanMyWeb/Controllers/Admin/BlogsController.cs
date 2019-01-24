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
using PlanMyWeb.Models;

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
            var blogcategories = _context.BlogCategories.AsEnumerable();
            ViewBag.Categories = new SelectList(blogcategories, "Id", "Title");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Blogs/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title,Image,HtmlDescription,PostDate,Categories")] BlogsViewModel blogsViewModel)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (blogsViewModel.Image != null && blogsViewModel.Image.Length > 0)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + blogsViewModel.Image.FileName;
                    UploadFile(blogsViewModel.Image, filename);
                }
                Blog row = new Blog { Image = filename, MediaType = blogsViewModel.MediaType, Title = blogsViewModel.Title, HtmlDescription = blogsViewModel.HtmlDescription, PostDate = blogsViewModel.PostDate };
                string[] catIds = Request.Form["Categories"].ToString().Split(',');

                var dbcats = _context.BlogCategories.Where(x => catIds.Contains(x.Id.ToString())).ToList();
                foreach (var dbcat in dbcats)
                    _context.BlogCategoryRelations.Add(new BlogCategoryRelation { Category = dbcat, Blog = row });
            
                _context.Blogs.Add(row);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogsViewModel);
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

            var row = _context.Blogs.Include(x => x.BlogCategoryRelations).Where(x => x.Id == id).FirstOrDefault();
            var blogcategories = _context.BlogCategories.AsEnumerable();
            var catslist = new SelectList(blogcategories, "Id", "Title");
            foreach (var item in catslist)
            {
                int itemValue = int.Parse(item.Value);
                item.Selected = row.BlogCategoryRelations.Where(x => x.Category.Id == itemValue).Count() > 0;
            }
            //ViewBag.Categories = selectfield;
            BlogsViewModel model = new BlogsViewModel { Id = row.Id, MediaType = row.MediaType, HtmlDescription = row.HtmlDescription, PostDate = row.PostDate, Title = row.Title, Categories = catslist };
            if (row == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Blogs/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Image,HtmlDescription,PostDate,Categories")] BlogViewModel blogViewModel)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Blogs.Where(x => x.Id == id).FirstOrDefault();
                if (blogViewModel.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + blogViewModel.Image.FileName;
                    UploadFile(blogViewModel.Image, filename);
                    row.Image = filename;
                }
                else
                    row.Image = row.Image;
                row.Title = blogViewModel.Title;
                row.HtmlDescription = blogViewModel.HtmlDescription;
                row.PostDate = blogViewModel.PostDate;
                if(row.BlogCategoryRelations!=null)
                    _context.BlogCategoryRelations.RemoveRange(row.BlogCategoryRelations);
                string[] catIds = Request.Form["Categories"].ToString().Split(',');

                var dbcats = _context.BlogCategories.Where(x => catIds.Contains(x.Id.ToString())).ToList();
                foreach (var dbcat in dbcats)
                    _context.BlogCategoryRelations.Add(new BlogCategoryRelation { Category = dbcat, Blog = row });
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

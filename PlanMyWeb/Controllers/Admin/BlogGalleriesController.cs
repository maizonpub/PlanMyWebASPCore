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
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin,Vendor")]
    public class BlogGalleriesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<Users> _userManager;
        public BlogGalleriesController(DbWebContext context, IHostingEnvironment hostingEnvironment, UserManager<Users> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _userManager = userManager;
        }

        [Route("Admin/BlogGalleries")]
        // GET: BlogGalleries
        public async Task<IActionResult> Index(int blogId)
        {
            var blogGallery = await _context.BlogGalleries.Where(x => x.Blog.Id == blogId).ToListAsync();
            if (User.IsInRole("Vendor"))
            {
                var userId = _userManager.GetUserId(User);
                blogGallery = blogGallery.Where(x => x.UserId == userId).ToList();
            }
            ViewBag.BlogId = blogId.ToString();
            return View(blogGallery);
        }
        [Route("Admin/BlogGalleries/Details/{id?}")]
        // GET: BlogGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogGallery = await _context.BlogGalleries.Include(x => x.Blog)
                 .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.BlogId = blogGallery.Blog.Id.ToString();
            if (blogGallery == null)
            {
                return NotFound();
            }

            return View(blogGallery);
        }
        [Route("Admin/BlogGalleries/Create")]
        // GET: BlogGalleries/Create
        public IActionResult Create(string blogId)
        {
            ViewBag.BlogId = blogId;
            return View();
        }

        // POST: BlogGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/BlogGalleries/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image")] BlogGalleryViewModel blogGalleryviewmodel, int blogId)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (blogGalleryviewmodel.Image != null && blogGalleryviewmodel.Image.Length > 0)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + blogGalleryviewmodel.Image.FileName;
                    UploadFile(blogGalleryviewmodel.Image, filename);
                }
                string userId = "";
                if (!string.IsNullOrEmpty(Request.Form["User"]))
                    userId = Request.Form["User"];
                else
                {
                    userId = _userManager.GetUserId(User);
                }
                var user = _context.Users.Find(userId);
                var blog = _context.Blogs.Find(blogId);
                BlogGallery blogGallery = new BlogGallery { Image = filename, Blog = blog };
                _context.Add(blogGallery);
                await _context.SaveChangesAsync();
                var parms = new Dictionary<string, string>
    {
        { "blogId", blogId.ToString() }
    };
                return RedirectToAction(nameof(Index), parms);

            }

            return View(blogGalleryviewmodel);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        [Route("Admin/BlogGalleries/Edit/{id?}")]
        // GET: BlogGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogGallery = _context.BlogGalleries.Include(x => x.Blog).Where(x => x.Id == id).FirstOrDefault();
            BlogGalleryViewModel model = new BlogGalleryViewModel { Id = blogGallery.Id };
            if (blogGallery == null)
            {
                return NotFound();
            }
            ViewBag.BlogId = blogGallery.Blog.Id.ToString();
            return View(model);
        }

        // POST: BlogGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/BlogGalleries/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Media")] BlogGalleryViewModel blogGalleryviewmodel, int blogId)
        {
            if (ModelState.IsValid)
            {
                var parms = new Dictionary<string, string>
    {
        { "blogId", blogId.ToString() }
    };
                string userId = "";
                if (!string.IsNullOrEmpty(Request.Form["User"]))
                    userId = Request.Form["User"];
                else
                {
                    userId = _userManager.GetUserId(User);
                }
                var user = _context.Users.Find(userId);
                var blog = _context.Blogs.Find(blogId);
                var row = _context.BlogGalleries.Where(x => x.Id == id).FirstOrDefault();
                if (blogGalleryviewmodel.Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Substring(4) + blogGalleryviewmodel.Image.FileName;
                    UploadFile(blogGalleryviewmodel.Image, filename);
                    row.Image = filename;
                }
                else
                    row.Image = row.Image;
                row.Blog = row.Blog;
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), parms);
            }
            return View(blogGalleryviewmodel);
        }
        [Route("Admin/BlogGalleries/Delete/{id?}")]
        // GET: BlogGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var blogGallery = await _context.BlogGalleries.Include(x => x.Blog)
               .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.BlogId = blogGallery.Blog.Id.ToString();
            if (blogGallery == null)
            {
                return NotFound();
            }

            return View(blogGallery);
        }

        // POST: BlogGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/BlogGalleries/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogGallery = _context.BlogGalleries.Include(x => x.Blog).Where(x => x.Id == id).FirstOrDefault();
            var parms = new Dictionary<string, string>
    {
        { "blogId", blogGallery.Blog.Id.ToString() }
    };
            _context.BlogGalleries.Remove(blogGallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), parms);
        }

        private bool BlogGalleryExists(int id)
        {
            return _context.BlogGalleries.Any(e => e.Id == id);
        }
    }
}

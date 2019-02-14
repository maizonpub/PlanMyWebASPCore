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
using PlanMyWeb.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class TopicGalleriesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TopicGalleriesController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [Route("Admin/TopicGalleries")]
        // GET: TopicGalleries
        public async Task<IActionResult> Index()
        {
            return View(await _context.TopicGalleries.ToListAsync());
        }

        [Route("Admin/TopicGalleries/Details/{id?}")]
        // GET: TopicGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicGallery = await _context.TopicGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topicGallery == null)
            {
                return NotFound();
            }

            return View(topicGallery);
        }

        [Route("Admin/TopicGalleries/Create")]
        // GET: TopicGalleries/Create
        public IActionResult Create()
        {
            var gallery = _context.TopicGalleries.ToList();
            ViewBag.Gallery = new SelectList(gallery, "Id", "Image");
            return View();
        }

        // POST: TopicGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/TopicGalleries/Create")]
        public async Task<IActionResult> Create([Bind("Id,Image")] TopicGalleryViewModel topicGallery)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (topicGallery.Image != null)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + topicGallery.Image.FileName ;
                    UploadFile(topicGallery.Image, filename);
                }
                if (topicGallery.Image != null)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + topicGallery.Image.FileName;
                    UploadFile(topicGallery.Image, filename);
                }
                TopicGallery TopicGallery = new TopicGallery { Id = topicGallery.Id, Image = filename};
                _context.TopicGalleries.Add(TopicGallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var gallery = _context.TopicGalleries.ToList();
            ViewBag.Gallery = new SelectList(gallery, "Id", "Image");
            return View(topicGallery);
            
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/TopicGalleries/Edit/{id?}")]
        // GET: TopicGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicGallery = await _context.TopicGalleries.FindAsync(id);
            if (topicGallery == null)
            {
                return NotFound();
            }
            var gallery = _context.TopicGalleries.ToList();
            ViewBag.Gallery = new SelectList(gallery, "Id", "Image");
            return View(topicGallery);
            
        }

        // POST: TopicGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/TopicGalleries/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] TopicGalleryViewModel topicGallery)
        {
            if (ModelState.IsValid)
            {
                var row = _context.TopicGalleries.Where(x => x.Id == id).FirstOrDefault();
                if (topicGallery.Image != null)
                {

                    string filename = Guid.NewGuid().ToString().Substring(4) + topicGallery.Image.FileName;
                    UploadFile(topicGallery.Image, filename);
                    row.Image = filename;
                }
                else
                    row.Image = row.Image;
                if (topicGallery.Image != null)
                {

                    string filename = Guid.NewGuid().ToString().Substring(4) + topicGallery.Image.FileName;
                    UploadFile(topicGallery.Image, filename);
                    row.Image = filename;
                }
                else
                    row.Image = row.Image;



                row.Id = topicGallery.Id;
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topicGallery);
        }

        [Route("Admin/TopicGalleries/Delete/{id?}")]
        // GET: TopicGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicGallery = await _context.TopicGalleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topicGallery == null)
            {
                return NotFound();
            }

            return View(topicGallery);
        }

        // POST: TopicGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/TopicGalleries/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topicGallery = await _context.TopicGalleries.FindAsync(id);
            _context.TopicGalleries.Remove(topicGallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicGalleryExists(int id)
        {
            return _context.TopicGalleries.Any(e => e.Id == id);
        }
    }
}

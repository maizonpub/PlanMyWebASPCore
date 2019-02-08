using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class CareerController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CareerController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
      
        [Route("Admin/Careers")]
        // GET: Careers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Careers.ToListAsync());
        }
        [Route("Admin/Careers/Details/{id?}")]
        // GET: Careers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var career = await _context.Careers.Include(x => x.Pages)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (career == null)
            {
                return NotFound();
            }
            
            return View(career);
        }
        [Route("Admin/Careers/Create")]
        // GET: Careers/Create
        public IActionResult Create()
        {
            var pages = _context.Pages.ToList();
            ViewBag.Pages = new SelectList(pages, "Id", "Title");
            return View();
        }

        // POST: Careers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Careers/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title,HtmlDescription,JobStatus,Pages,WideImage,Thumb")] CareerViewModel career)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (career.WideImage != null)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + career.WideImage.FileName ;
                    UploadFile(career.WideImage, filename);
                }
                if (career.Thumb != null)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + career.Thumb.FileName;
                    UploadFile(career.Thumb, filename);
                }
                Career careers = new Career { Id = career.Id, Title = career.Title, HtmlDescription = career.HtmlDescription, WideImage = filename, Thumb = filename , JobStatus = career.JobStatus, Pages = career.Pages};
                _context.Careers.Add(careers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var pages = _context.Pages.ToList();
            ViewBag.Pages = new SelectList(pages, "Id", "Title");
            return View(career);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        [Route("Admin/Careers/Edit/{id?}")]
        // GET: Careers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var career = _context.Careers.Include(x => x.Pages).Where(x => x.Id == id).FirstOrDefault();
            CareerViewModel model = new CareerViewModel { Id = career.Id, Title = career.Title, HtmlDescription = career.HtmlDescription, JobStatus = career.JobStatus, Pages = career.Pages};
            if (career == null)
            {
                return NotFound();
            }
            var pages = _context.Pages.ToList();
            ViewBag.Pages = new SelectList(pages, "Id", "Title");
            return View(model);
        }

        // POST: Careers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Careers/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HtmlDescription,JobStatus,Pages,Thumb,WideImage")] CareerViewModel career)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Careers.Where(x => x.Id == id).FirstOrDefault();
                if (career.WideImage != null)
                {

                    string filename = Guid.NewGuid().ToString().Substring(4) + career.WideImage.FileName;
                    UploadFile(career.WideImage, filename);
                    row.WideImage = filename;
                }
                else
                    row.WideImage = row.WideImage;
                if (career.Thumb != null)
                {

                    string filename = Guid.NewGuid().ToString().Substring(4) + career.Thumb.FileName;
                    UploadFile(career.Thumb, filename);
                    row.Thumb = filename;
                }
                else
                    row.Thumb = row.Thumb;



                row.Id = career.Id;
                row.Title = career.Title;
                row.HtmlDescription = career.HtmlDescription;
                row.JobStatus = career.JobStatus;
                row.Pages = career.Pages;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(career);
        }

        [Route("Admin/Careers/Delete/{id?}")]
        // GET: Careers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers.Include(x => x.Pages)
                .FirstOrDefaultAsync(m => m.Id == id);
            //ViewBag.PageId = career.Pages.ToString();
            //if (career == null)
            //{
            //    return NotFound();
            //}

            return View(career);
        }

        // POST: Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Careers/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var career = _context.Careers.Include(x => x.Pages).Where(x => x.Id == id).FirstOrDefault();
            _context.Careers.Remove(career);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

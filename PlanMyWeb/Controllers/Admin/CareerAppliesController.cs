using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize (Roles = "Admin")]
    public class CareerAppliesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CareerAppliesController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [Route("Admin/CareerApplies")]
        // GET: CareerApplies
        public async Task<IActionResult> Index()
        {
            return View(await _context.CareerApplies.ToListAsync());
        }
        [Route("Admin/CareerApplies/Details/{id?}")]
        // GET: CareerApplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careerapplies = await _context.CareerApplies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (careerapplies == null)
            {
                return NotFound();
            }

            return View(careerapplies);
        }
        [Route("Admin/CareerApplies/Create")]
        // GET: CareerApplies/Create
        public IActionResult Create()
        {
            var pages = _context.Pages.ToList();
            ViewBag.Pages = new SelectList(pages, "Id", "Title");
            return View();
        }

        // POST: CareerApplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/CareerApplies/Create")]
        public async Task<IActionResult> Create([Bind("Id,Career,CV,Name,Phone,Email")] CareerAppliesViewModel careerapplies)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (careerapplies.CV != null)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + careerapplies.CV.FileName;
                    UploadFile(careerapplies.CV, filename);
                }
                
                CareerApplies careerApplies = new CareerApplies { Id = careerapplies.Id, Career = careerapplies.Career, CV = filename, Name = careerapplies.Name, Email = careerapplies.Email, Phone = careerapplies.Phone};
                _context.CareerApplies.Add(careerApplies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var pages = _context.Pages.ToList();
            ViewBag.Pages = new SelectList(pages, "Id", "Title");
            return View(careerapplies);
        }

        private void UploadFile(string cV, string filename)
        {
            throw new NotImplementedException();
        }

        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/CareerApplies/Edit/{id?}")]
        // GET: CareerApplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careerapplies = _context.CareerApplies.Include(x => x.Career.Pages).Where(x => x.Id == id).FirstOrDefault();
            CareerAppliesViewModel model = new CareerAppliesViewModel { Id = careerapplies.Id, Career = careerapplies.Career, Name = careerapplies.Name, Phone = careerapplies.Phone, Email = careerapplies.Email };
            if (careerapplies == null)
            {
                return NotFound();
            }
            var pages = _context.Pages.ToList();
            ViewBag.Pages = new SelectList(pages, "Id", "Title");
            return View(model);
        }

        // POST: CareerApplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/CareerApplies/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Career,CV,Name,Phone,Email")] CareerAppliesViewModel careerapplies)
        {
            if (ModelState.IsValid)
            {
                var row = _context.CareerApplies.Where(x => x.Id == id).FirstOrDefault();
                if (careerapplies.CV != null)
                {

                    string filename = Guid.NewGuid().ToString().Substring(4) + careerapplies.CV.FileName;
                    UploadFile(careerapplies.CV, filename);
                    row.CV = filename;
                }
                else
                    row.CV = row.CV;
                

                row.Id = careerapplies.Id;
                row.Career = careerapplies.Career;
                row.Name = careerapplies.Name;
                row.Phone = careerapplies.Phone;
                row.Email = careerapplies.Email;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(careerapplies);
        }

        [Route("Admin/CareerApplies/Delete/{id?}")]
        // GET: CareerApplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careerapplies = await _context.CareerApplies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (careerapplies == null)
            {
                return NotFound();
            }

            return View(careerapplies);
        }

        // POST: CareerApplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/CareerApplies/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var careerapplies = await _context.CareerApplies.FindAsync(id);
            _context.CareerApplies.Remove(careerapplies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
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
using Microsoft.EntityFrameworkCore;
using PlanMyWeb.Models;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UsersController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        
        [Route("Admin/Users")]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<UsersViewModel> model = new List<UsersViewModel>();
            var users = await _context.Users.ToListAsync();
            foreach(var user in users)
            {
                model.Add(new UsersViewModel {  Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Gender = user.Gender,  UserType = user.UserType, PhoneNumber = user.PhoneNumber, Email = user.Email, Username = user.UserName });
            }
            return View(model);
        }
        [Route("Admin/Users/Details/{id?}")]
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(Id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }
        [Route("Admin/Users/Create")]
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Users/Create")]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Age,Gender,Address,City,Country,UserType,CreationDate,Image,PhoneNumber,Email,Username")] UsersViewModel users)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (users.Image != null)
                {
                    filename = Guid.NewGuid().ToString().Substring(4) + users.Image.FileName;
                    UploadFile(users.Image, filename);
                }
                Users user = new Users { Id = users.Id, FirstName = users.FirstName, LastName = users.LastName, Age = users.Age, Gender = users.Gender, Address = users.Address, City = users.City, Country = users.Country, UserType = users.UserType, CreationDate = users.CreationDate, Image = filename, UserName = users.Username, PhoneNumber = users.PhoneNumber, Email = users.Email };
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }

        [Route("Admin/Users/Edit/{id?}")]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            UsersViewModel model = new UsersViewModel { Id = users.Id, Gender = users.Gender, UserType = users.UserType, Address = users.Address, Age = users.Age, City = users.City, Country = users.Country, CreationDate = users.CreationDate, FirstName = users.FirstName, LastName = users.LastName, ImageString = users.Image, Username = users.UserName, Email = users.Email, PhoneNumber = users.PhoneNumber};
            if (users == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Users/Edit/{id?}")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Age,Gender,Address,City,Country,UserType,CreationDate,Image,Username,Email,PhoneNumber")] UsersViewModel users)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                if (users.Image != null)
                {

                    string filename = Guid.NewGuid().ToString().Substring(4) + users.Image.FileName;
                    UploadFile(users.Image, filename);
                    row.Image = filename;
                }
                else
                    row.Id = row.Id;
                    row.Image = row.Image;
                row.FirstName = users.FirstName;
                row.LastName = users.LastName;
                row.UserName = users.Username;
                row.Email = users.Email;
                row.PhoneNumber = users.PhoneNumber;
                row.Age = users.Age;
                row.Address = users.Address;
                row.City = users.City;
                row.Country = users.Country;
                row.UserType = users.UserType;
                row.CreationDate = users.CreationDate;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }
        [Route("Admin/Users/Delete/{id?}")]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Users/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
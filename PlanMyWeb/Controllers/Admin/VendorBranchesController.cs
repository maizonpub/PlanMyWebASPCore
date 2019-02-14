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
using Microsoft.AspNetCore.Identity;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin,Vendor")]
    public class VendorBranchesController : Controller
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<Users> _userManager;
        public VendorBranchesController(DbWebContext context, IHostingEnvironment hostingEnvironment, UserManager<Users> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _userManager = userManager;
        }

        
        [Route("Admin/VendorBranches")]
        // GET: VendorBranches
        public async Task<IActionResult> Index(int itemId)
        {
            var vendorBranches = await _context.VendorBranches.Where(x => x.VendorItemId == itemId).ToListAsync();
            if (User.IsInRole("Vendor"))
            {
                var userId = _userManager.GetUserId(User);
                vendorBranches = vendorBranches.Where(x => x.UserId == userId).ToList();
            }
            ViewBag.ItemId = itemId.ToString();
            return View(vendorBranches);
        }

        [Route("Admin/VendorBranches/Details/{id?}")]
        // GET: VendorBranches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorBranches = await _context.VendorBranches.Include(x => x.VendorItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.ItemId = vendorBranches.VendorItemId.ToString();
            if (vendorBranches == null)
            {
                return NotFound();
            }

            return View(vendorBranches);
        }
        [Route("Admin/VendorBranches/Create")]
        // GET: VendorBranches/Create
        public IActionResult Create(string itemId)
        {
            ViewBag.ItemId = itemId;
            return View();
        }
        [Route("Admin/VendorBranches/Create")]
        // POST: VendorBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Address,PhoneNumber,Latitude,Longitude,VendorItem")] VendorBranches vendorBranches, int itemId)
        {
            if (ModelState.IsValid)
            {
                vendorBranches.VendorItemId = itemId;
                _context.Add(vendorBranches);
                await _context.SaveChangesAsync();
                var parms = new Dictionary<string, string>
    {
        { "itemId", itemId.ToString() }
    };
                return RedirectToAction(nameof(Index), parms);

            }

            return View(vendorBranches);
        }
        [Route("Admin/VendorBranches/Edit/{id?}")]
        // GET: VendorBranches/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var vendorBranches = await _context.VendorBranches.FindAsync(id);
            var vendorBranches = _context.VendorBranches.Include(x => x.VendorItem).Where(x => x.Id == id).FirstOrDefault();
            
            if (vendorBranches == null)
            {
                return NotFound();
            }
            ViewBag.ItemId = vendorBranches.VendorItemId.ToString();
            return View(vendorBranches);
        }
        
        // POST: VendorBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorBranches/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Address,PhoneNumber,Latitude,Longitude,VendorItem")] VendorBranches vendorBranches,int itemId)
        {
            if (ModelState.IsValid)
            {
                var parms = new Dictionary<string, string>
    {
        { "itemId", itemId.ToString() }
    };
                vendorBranches.VendorItemId = itemId;
                _context.Update(vendorBranches);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), parms);
            }
            ViewBag.ItemId = itemId.ToString();
            return View(vendorBranches);
        }
        [Route("Admin/VendorBranches/Delete/{id?}")]
        // GET: VendorBranches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorBranches = await _context.VendorBranches
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.ItemId = vendorBranches.VendorItemId.ToString();
            if (vendorBranches == null)
            {
                return NotFound();
            }

            return View(vendorBranches);
        }

        // POST: VendorBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorBranches/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorItem = _context.VendorBranches.Include(x => x.VendorItem).Where(x => x.Id == id).FirstOrDefault();
            var parms = new Dictionary<string, string>
    {
        { "itemId", vendorItem.VendorItemId.ToString() }
    };
            _context.VendorBranches.Remove(vendorItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), parms);
        }

        private bool VendorBranchesExists(int id)
        {
            return _context.VendorBranches.Any(e => e.Id == id);
        }
    }
}

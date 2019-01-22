using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Authorization;

namespace PlanMyWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class VendorTypeValuesController : Controller
    {
        private readonly DbWebContext _context;

        public VendorTypeValuesController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/VendorTypeValues")]
        // GET: VendorTypeValues
        public async Task<IActionResult> Index(int typeId)
        {
            var type = _context.VendorTypes.Find(typeId);
            ViewData["Title"] = type.Title;
            ViewBag.TypeId = typeId.ToString();
            return View(await _context.VendorTypeValues.Where(x=>x.VendorType.Id == typeId).ToListAsync());
        }
        [Route("Admin/VendorTypeValues/Details/{id?}")]
        // GET: VendorTypeValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorTypeValue = _context.VendorTypeValues.Include(x => x.VendorType).Where(x => x.Id == id).FirstOrDefault();
            ViewBag.TypeId = vendorTypeValue.VendorType.Id.ToString();
            if (vendorTypeValue == null)
            {
                return NotFound();
            }

            return View(vendorTypeValue);
        }
        [Route("Admin/VendorTypeValues/Create")]
        // GET: VendorTypeValues/Create
        public IActionResult Create(string typeId)
        {
            ViewBag.TypeId = typeId;
            return View();
        }

        // POST: VendorTypeValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypeValues/Create")]
        public async Task<IActionResult> Create([Bind("Id,Title")] VendorTypeValue vendorTypeValue, int typeId)
        {
            if (ModelState.IsValid)
            {
                var vendortype = _context.VendorTypes.Find(typeId);
                vendorTypeValue.VendorType = vendortype;
                _context.Add(vendorTypeValue);
                await _context.SaveChangesAsync();
                var parms = new Dictionary<string, string>
    {
        { "typeId", typeId.ToString() }
    };
                return RedirectToAction(nameof(Index),parms);
            }
            return View(vendorTypeValue);
        }
        [Route("Admin/VendorTypeValues/Edit/{id?}")]
        // GET: VendorTypeValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorTypeValue = _context.VendorTypeValues.Include(x=>x.VendorType).Where(x=>x.Id == id).FirstOrDefault();
            if (vendorTypeValue == null)
            {
                return NotFound();
            }
            ViewBag.TypeId = vendorTypeValue.VendorType.Id.ToString();
            return View(vendorTypeValue);
        }

        // POST: VendorTypeValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypeValues/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] VendorTypeValue vendorTypeValue, int typeId)
        {
            if (id != vendorTypeValue.Id)
            {
                return NotFound();
            }
            var parms = new Dictionary<string, string>
    {
        { "typeId", typeId.ToString() }
    };
            if (ModelState.IsValid)
            {
                try
                {
                    var vendortype = _context.VendorTypes.Find(typeId);
                    vendorTypeValue.VendorType = vendortype;
                    _context.Update(vendorTypeValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorTypeValueExists(vendorTypeValue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), parms);
            }
            return View(vendorTypeValue);
        }
        [Route("Admin/VendorTypeValues/Delete/{id?}")]
        // GET: VendorTypeValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorTypeValue = _context.VendorTypeValues.Include(x => x.VendorType).Where(x => x.Id == id).FirstOrDefault();
            ViewBag.TypeId = vendorTypeValue.VendorType.Id.ToString();
            if (vendorTypeValue == null)
            {
                return NotFound();
            }

            return View(vendorTypeValue);
        }

        // POST: VendorTypeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/VendorTypeValues/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorTypeValue = await _context.VendorTypeValues.Include(x=>x.VendorType)
                .FirstOrDefaultAsync(m => m.Id == id);
            var parms = new Dictionary<string, string>
    {
        { "typeId", vendorTypeValue.VendorType.Id.ToString() }
    };
            _context.VendorTypeValues.Remove(vendorTypeValue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), parms);
        }

        private bool VendorTypeValueExists(int id)
        {
            return _context.VendorTypeValues.Any(e => e.Id == id);
        }
    }
}

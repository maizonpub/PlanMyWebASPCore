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
    public class PaymentSettingsController : Controller
    {
        private readonly DbWebContext _context;

        public PaymentSettingsController(DbWebContext context)
        {
            _context = context;
        }
        [Route("Admin/PaymentSettings/Index")]
        // GET: PaymentSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentSettings.ToListAsync());
        }
        [Route("Admin/PaymentSettings/Details/{id?}")]
        // GET: PaymentSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentSetting = await _context.PaymentSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentSetting == null)
            {
                return NotFound();
            }

            return View(paymentSetting);
        }
        [Route("Admin/PaymentSettings/Create")]
        // GET: PaymentSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/PaymentSettings/Create")]
        public async Task<IActionResult> Create([Bind("Id,ProfileId,AccessKey,SecuritySign,PaymentType,RecurringFrequency")] PaymentSetting paymentSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentSetting);
        }

        // GET: PaymentSettings/Edit/5
        [Route("Admin/PaymentSettings/Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentSetting = await _context.PaymentSettings.FindAsync(id);
            if (paymentSetting == null)
            {
                return NotFound();
            }
            return View(paymentSetting);
        }

        // POST: PaymentSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/PaymentSettings/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfileId,AccessKey,SecuritySign,PaymentType,RecurringFrequency")] PaymentSetting paymentSetting)
        {
            if (id != paymentSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentSettingExists(paymentSetting.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paymentSetting);
        }

        // GET: PaymentSettings/Delete/5
        [Route("Admin/PaymentSettings/Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentSetting = await _context.PaymentSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentSetting == null)
            {
                return NotFound();
            }

            return View(paymentSetting);
        }

        // POST: PaymentSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Admin/PaymentSettings/Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentSetting = await _context.PaymentSettings.FindAsync(id);
            _context.PaymentSettings.Remove(paymentSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentSettingExists(int id)
        {
            return _context.PaymentSettings.Any(e => e.Id == id);
        }
    }
}

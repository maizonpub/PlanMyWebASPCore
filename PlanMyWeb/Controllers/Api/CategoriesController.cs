using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace PlanMyWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DbWebContext _context;

        public CategoriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<VendorCategory> GetVendorCategories()
        {
            var cats = _context.VendorCategories;
            foreach(var row in cats)
            {
                row.Image = "https://planmy.me/Media/" + row.Image;
            }
            return cats;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorCategory = await _context.VendorCategories.FindAsync(id);

            if (vendorCategory == null)
            {
                return NotFound();
            }

            return Ok(vendorCategory);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendorCategory([FromRoute] int id, [FromBody] VendorCategory vendorCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendorCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostVendorCategory([FromBody] VendorCategory vendorCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VendorCategories.Add(vendorCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendorCategory", new { id = vendorCategory.Id }, vendorCategory);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendorCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorCategory = await _context.VendorCategories.FindAsync(id);
            if (vendorCategory == null)
            {
                return NotFound();
            }

            _context.VendorCategories.Remove(vendorCategory);
            await _context.SaveChangesAsync();

            return Ok(vendorCategory);
        }

        private bool VendorCategoryExists(int id)
        {
            return _context.VendorCategories.Any(e => e.Id == id);
        }
    }
}
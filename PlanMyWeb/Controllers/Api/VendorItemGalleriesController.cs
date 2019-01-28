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
    public class VendorItemGalleriesController : ControllerBase
    {
        private readonly DbWebContext _context;

        public VendorItemGalleriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/VendorItemGalleries
        [HttpGet]
        public IEnumerable<VendorItemGallery> GetVendorItemGalleries()
        {
            return _context.VendorItemGalleries;
        }

        // GET: api/VendorItemGalleries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorItemGallery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorItemGallery = await _context.VendorItemGalleries.FindAsync(id);

            if (vendorItemGallery == null)
            {
                return NotFound();
            }

            return Ok(vendorItemGallery);
        }

        // PUT: api/VendorItemGalleries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendorItemGallery([FromRoute] int id, [FromBody] VendorItemGallery vendorItemGallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorItemGallery.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendorItemGallery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorItemGalleryExists(id))
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

        // POST: api/VendorItemGalleries
        [HttpPost]
        public async Task<IActionResult> PostVendorItemGallery([FromBody] VendorItemGallery vendorItemGallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VendorItemGalleries.Add(vendorItemGallery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendorItemGallery", new { id = vendorItemGallery.Id }, vendorItemGallery);
        }

        // DELETE: api/VendorItemGalleries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendorItemGallery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorItemGallery = await _context.VendorItemGalleries.FindAsync(id);
            if (vendorItemGallery == null)
            {
                return NotFound();
            }

            _context.VendorItemGalleries.Remove(vendorItemGallery);
            await _context.SaveChangesAsync();

            return Ok(vendorItemGallery);
        }

        private bool VendorItemGalleryExists(int id)
        {
            return _context.VendorItemGalleries.Any(e => e.Id == id);
        }
    }
}
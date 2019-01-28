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
    public class VendorTypesController : ControllerBase
    {
        private readonly DbWebContext _context;

        public VendorTypesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/VendorTypes
        [HttpGet]
        public IEnumerable<VendorType> GetVendorTypes()
        {
            var types = _context.VendorTypes.Include(x => x.VendorTypeValues);
            return types;
        }

        // GET: api/VendorTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorType = await _context.VendorTypes.FindAsync(id);

            if (vendorType == null)
            {
                return NotFound();
            }

            return Ok(vendorType);
        }

        // PUT: api/VendorTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendorType([FromRoute] int id, [FromBody] VendorType vendorType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorType.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendorType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorTypeExists(id))
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

        // POST: api/VendorTypes
        [HttpPost]
        public async Task<IActionResult> PostVendorType([FromBody] VendorType vendorType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VendorTypes.Add(vendorType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendorType", new { id = vendorType.Id }, vendorType);
        }

        // DELETE: api/VendorTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendorType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorType = await _context.VendorTypes.FindAsync(id);
            if (vendorType == null)
            {
                return NotFound();
            }

            _context.VendorTypes.Remove(vendorType);
            await _context.SaveChangesAsync();

            return Ok(vendorType);
        }

        private bool VendorTypeExists(int id)
        {
            return _context.VendorTypes.Any(e => e.Id == id);
        }
    }
}
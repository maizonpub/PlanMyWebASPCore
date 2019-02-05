using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace PlanMyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorItemReviewsController : ControllerBase
    {
        private readonly DbWebContext _context;

        public VendorItemReviewsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/VendorItemReviews
        [HttpGet]
        public IEnumerable<VendorItemReview> GetVendorItemReviews()
        {
            return _context.VendorItemReviews;
        }

        // GET: api/VendorItemReviews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorItemReview([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorItemReview = await _context.VendorItemReviews.FindAsync(id);

            if (vendorItemReview == null)
            {
                return NotFound();
            }

            return Ok(vendorItemReview);
        }

        // PUT: api/VendorItemReviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendorItemReview([FromRoute] long id, [FromBody] VendorItemReview vendorItemReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorItemReview.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendorItemReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorItemReviewExists(id))
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

        // POST: api/VendorItemReviews
        [HttpPost]
        public async Task<IActionResult> PostVendorItemReview([FromBody] VendorItemReview vendorItemReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VendorItemReviews.Add(vendorItemReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendorItemReview", new { id = vendorItemReview.Id }, vendorItemReview);
        }

        // DELETE: api/VendorItemReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendorItemReview([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorItemReview = await _context.VendorItemReviews.FindAsync(id);
            if (vendorItemReview == null)
            {
                return NotFound();
            }

            _context.VendorItemReviews.Remove(vendorItemReview);
            await _context.SaveChangesAsync();

            return Ok(vendorItemReview);
        }

        private bool VendorItemReviewExists(long id)
        {
            return _context.VendorItemReviews.Any(e => e.Id == id);
        }
    }
}
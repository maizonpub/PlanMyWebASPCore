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
    public class OffersGalleriesController : ControllerBase
    {
        private readonly DbWebContext _context;

        public OffersGalleriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/OffersGalleries
        [HttpGet]
        public IEnumerable<OffersGallery> GetOffersGalleries()
        {
            return _context.OffersGalleries;
        }

        // GET: api/OffersGalleries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffersGallery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var offersGallery = await _context.OffersGalleries.FindAsync(id);

            if (offersGallery == null)
            {
                return NotFound();
            }

            return Ok(offersGallery);
        }

        // PUT: api/OffersGalleries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffersGallery([FromRoute] int id, [FromBody] OffersGallery offersGallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offersGallery.Id)
            {
                return BadRequest();
            }

            _context.Entry(offersGallery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OffersGalleryExists(id))
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

        // POST: api/OffersGalleries
        [HttpPost]
        public async Task<IActionResult> PostOffersGallery([FromBody] OffersGallery offersGallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OffersGalleries.Add(offersGallery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffersGallery", new { id = offersGallery.Id }, offersGallery);
        }

        // DELETE: api/OffersGalleries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffersGallery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var offersGallery = await _context.OffersGalleries.FindAsync(id);
            if (offersGallery == null)
            {
                return NotFound();
            }

            _context.OffersGalleries.Remove(offersGallery);
            await _context.SaveChangesAsync();

            return Ok(offersGallery);
        }

        private bool OffersGalleryExists(int id)
        {
            return _context.OffersGalleries.Any(e => e.Id == id);
        }
    }
}
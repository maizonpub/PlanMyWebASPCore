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
    public class VendorItemsController : ControllerBase
    {
        private readonly DbWebContext _context;

        public VendorItemsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/VendorItems
        [HttpGet]
        public IEnumerable<VendorItem> GetVendorItems()
        {
            return _context.VendorItems.Include(x=>x.VendorItemReviews).Include(x => x.Gallery);
        }
        [HttpGet("{CategoryId}")]
        public IEnumerable<VendorItem> GetVendorItems([FromRoute] int CategoryId)
        {
            return _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Where(x=> x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0);
        }
        [HttpGet("Featured/{CategoryId}")]
        public IEnumerable<VendorItem> GetFeaturedVendorItems([FromRoute] int CategoryId)
        {
            return _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Where(x => x.Categories.Where(y => y.VendorCategory.Id == CategoryId).Count() > 0 && x.IsFeatured == true);
        }
        [HttpGet("Featured")]
        public IEnumerable<VendorItem> GetFeaturedVendorItems()
        {
            return _context.VendorItems.Include(x => x.VendorItemReviews).Include(x => x.Gallery).Where(x => x.IsFeatured == true);
        }
        [HttpGet("Favorites/{UserId}")]
        public IEnumerable<VendorItem> GetFavoritesVendorItems([FromRoute] string UserId)
        {
            return _context.VendorItems.Include(x => x.VendorItemReviews).Include(x=>x.Gallery).Where(x => x.WishLists!=null && x.WishLists.Where(y=>y.UserId == UserId).Count()>0);
        }
        [HttpPost]
        public IEnumerable<VendorItem> GetVendorItems(VendorItemSearch Search)
        {
            return _context.VendorItems.Include(x => x.VendorItemReviews).Where(x=>x.Categories.Where(y=>y.VendorCategory.Id == Search.CategoryId).Count()>0 && x.VendorItemTypeValues.Where(y=>Search.Values.Contains(y.VendorTypeValue)).Count()>0);
        }
        // GET: api/VendorItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorItem = await _context.VendorItems.FindAsync(id);

            if (vendorItem == null)
            {
                return NotFound();
            }

            return Ok(vendorItem);
        }

        // PUT: api/VendorItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendorItem([FromRoute] int id, [FromBody] VendorItem vendorItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendorItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendorItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorItemExists(id))
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

        // POST: api/VendorItems
        [HttpPost]
        public async Task<IActionResult> PostVendorItem([FromBody] VendorItem vendorItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VendorItems.Add(vendorItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendorItem", new { id = vendorItem.Id }, vendorItem);
        }

        // DELETE: api/VendorItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendorItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorItem = await _context.VendorItems.FindAsync(id);
            if (vendorItem == null)
            {
                return NotFound();
            }

            _context.VendorItems.Remove(vendorItem);
            await _context.SaveChangesAsync();

            return Ok(vendorItem);
        }

        private bool VendorItemExists(int id)
        {
            return _context.VendorItems.Any(e => e.Id == id);
        }
    }
}
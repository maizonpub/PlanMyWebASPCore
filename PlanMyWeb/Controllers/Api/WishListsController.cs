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
    public class WishListsController : ControllerBase
    {
        private readonly DbWebContext _context;

        public WishListsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/WishLists
        [HttpGet]
        public IEnumerable<WishList> GetWishLists()
        {
            return _context.WishLists;
        }

        // GET: api/WishLists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wishList = await _context.WishLists.FindAsync(id);

            if (wishList == null)
            {
                return NotFound();
            }

            return Ok(wishList);
        }

        // PUT: api/WishLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishList([FromRoute] int id, [FromBody] WishList wishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wishList.Id)
            {
                return BadRequest();
            }

            _context.Entry(wishList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishListExists(id))
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

        // POST: api/WishLists
        [HttpPost]
        public async Task<IActionResult> PostWishList([FromBody] WishList wishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WishLists.Add(wishList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWishList", new { id = wishList.Id }, wishList);
        }

        // DELETE: api/WishLists/5
        [HttpDelete("{itemId}/{UserId}")]
        public async Task<IActionResult> DeleteWishList([FromRoute] int itemId, string UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wishList = _context.WishLists.Where(x=>x.VendorItemId == itemId && x.UserId == UserId).FirstOrDefault();
            if (wishList == null)
            {
                return NotFound();
            }

            _context.WishLists.Remove(wishList);
            await _context.SaveChangesAsync();

            return Ok(wishList);
        }

        private bool WishListExists(int id)
        {
            return _context.WishLists.Any(e => e.Id == id);
        }
    }
}
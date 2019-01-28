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
    public class GuestListsController : ControllerBase
    {
        private readonly DbWebContext _context;

        public GuestListsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/GuestLists
        [HttpGet]
        public IEnumerable<GuestList> GetGuestLists(string UserId, int? tableId)
        {
            var results = _context.GuestLists.AsQueryable();
            if (!string.IsNullOrEmpty(UserId))
            {
                results =  results.Where(x => x.User.Id == UserId && x.GuestStatus == GuestStatus.Accepted);
                if (tableId != null)
                    results = results.Where(x => x.GuestListTablesId == tableId);
            }
            return results.Include(x => x.GuestListTables);
        }

        // GET: api/GuestLists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestList = await _context.GuestLists.FindAsync(id);

            if (guestList == null)
            {
                return NotFound();
            }

            return Ok(guestList);
        }

        // PUT: api/GuestLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuestList([FromRoute] int id, [FromBody] GuestList guestList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guestList.Id)
            {
                return BadRequest();
            }

            _context.Entry(guestList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestListExists(id))
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

        // POST: api/GuestLists
        [HttpPost]
        public async Task<IActionResult> PostGuestList([FromBody] GuestList guestList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GuestLists.Add(guestList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuestList", new { id = guestList.Id }, guestList);
        }

        // DELETE: api/GuestLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestList = await _context.GuestLists.FindAsync(id);
            if (guestList == null)
            {
                return NotFound();
            }

            _context.GuestLists.Remove(guestList);
            await _context.SaveChangesAsync();

            return Ok(guestList);
        }

        private bool GuestListExists(int id)
        {
            return _context.GuestLists.Any(e => e.Id == id);
        }
    }
}
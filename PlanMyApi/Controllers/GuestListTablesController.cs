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
    public class GuestListTablesController : ControllerBase
    {
        private readonly DbWebContext _context;

        public GuestListTablesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/GuestListTables
        [HttpGet]
        public IEnumerable<GuestListTables> GetGuestListTables(string UserId)
        {
            var results = _context.GuestListTables.AsQueryable();
            if (!string.IsNullOrEmpty(UserId))
            {
                results = results.Where(x => x.User.Id == UserId);
            }
            return results;
        }

        // GET: api/GuestListTables/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestListTables([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestListTables = await _context.GuestListTables.FindAsync(id);

            if (guestListTables == null)
            {
                return NotFound();
            }

            return Ok(guestListTables);
        }

        // PUT: api/GuestListTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuestListTables([FromRoute] int id, [FromBody] GuestListTables guestListTables)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guestListTables.Id)
            {
                return BadRequest();
            }

            _context.Entry(guestListTables).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestListTablesExists(id))
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

        // POST: api/GuestListTables
        [HttpPost]
        public async Task<IActionResult> PostGuestListTables([FromBody] GuestListTables guestListTables)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GuestListTables.Add(guestListTables);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuestListTables", new { id = guestListTables.Id }, guestListTables);
        }

        // DELETE: api/GuestListTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestListTables([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestListTables = await _context.GuestListTables.FindAsync(id);
            if (guestListTables == null)
            {
                return NotFound();
            }

            _context.GuestListTables.Remove(guestListTables);
            await _context.SaveChangesAsync();

            return Ok(guestListTables);
        }

        private bool GuestListTablesExists(int id)
        {
            return _context.GuestListTables.Any(e => e.Id == id);
        }
    }
}
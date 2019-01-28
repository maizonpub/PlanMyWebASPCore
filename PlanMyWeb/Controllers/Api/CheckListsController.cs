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
    public class CheckListsController : ControllerBase
    {
        private readonly DbWebContext _context;

        public CheckListsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/CheckLists
        [HttpGet]
        public IEnumerable<CheckList> GetCheckLists()
        {
            return _context.CheckLists;
        }

        // GET: api/CheckLists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheckList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkList = await _context.CheckLists.FindAsync(id);

            if (checkList == null)
            {
                return NotFound();
            }

            return Ok(checkList);
        }

        // PUT: api/CheckLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckList([FromRoute] int id, [FromBody] CheckList checkList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checkList.Id)
            {
                return BadRequest();
            }

            _context.Entry(checkList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckListExists(id))
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

        // POST: api/CheckLists
        [HttpPost]
        public async Task<IActionResult> PostCheckList([FromBody] CheckList checkList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CheckLists.Add(checkList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckList", new { id = checkList.Id }, checkList);
        }

        // DELETE: api/CheckLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkList = await _context.CheckLists.FindAsync(id);
            if (checkList == null)
            {
                return NotFound();
            }

            _context.CheckLists.Remove(checkList);
            await _context.SaveChangesAsync();

            return Ok(checkList);
        }

        private bool CheckListExists(int id)
        {
            return _context.CheckLists.Any(e => e.Id == id);
        }
    }
}
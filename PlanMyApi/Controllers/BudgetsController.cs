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
    public class BudgetsController : ControllerBase
    {
        private readonly DbWebContext _context;

        public BudgetsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/Budgets
        [HttpGet]
        public IEnumerable<Budget> GetBudgets()
        {
            return _context.Budgets;
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudget([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var budget = await _context.Budgets.FindAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            return Ok(budget);
        }

        // PUT: api/Budgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget([FromRoute] int id, [FromBody] Budget budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != budget.Id)
            {
                return BadRequest();
            }

            _context.Entry(budget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(id))
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

        // POST: api/Budgets
        [HttpPost]
        public async Task<IActionResult> PostBudget([FromBody] Budget budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudget", new { id = budget.Id }, budget);
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();

            return Ok(budget);
        }

        private bool BudgetExists(int id)
        {
            return _context.Budgets.Any(e => e.Id == id);
        }
    }
}
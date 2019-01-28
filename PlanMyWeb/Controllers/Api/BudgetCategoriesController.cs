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
    public class BudgetCategoriesController : ControllerBase
    {
        private readonly DbWebContext _context;

        public BudgetCategoriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/BudgetCategories
        [HttpGet]
        public IEnumerable<BudgetCategory> GetBudgetCategories()
        {
            return _context.BudgetCategories;
        }

        // GET: api/BudgetCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var budgetCategory = await _context.BudgetCategories.FindAsync(id);

            if (budgetCategory == null)
            {
                return NotFound();
            }

            return Ok(budgetCategory);
        }

        // PUT: api/BudgetCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudgetCategory([FromRoute] int id, [FromBody] BudgetCategory budgetCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != budgetCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(budgetCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetCategoryExists(id))
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

        // POST: api/BudgetCategories
        [HttpPost]
        public async Task<IActionResult> PostBudgetCategory([FromBody] BudgetCategory budgetCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BudgetCategories.Add(budgetCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudgetCategory", new { id = budgetCategory.Id }, budgetCategory);
        }

        // DELETE: api/BudgetCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudgetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var budgetCategory = await _context.BudgetCategories.FindAsync(id);
            if (budgetCategory == null)
            {
                return NotFound();
            }

            _context.BudgetCategories.Remove(budgetCategory);
            await _context.SaveChangesAsync();

            return Ok(budgetCategory);
        }

        private bool BudgetCategoryExists(int id)
        {
            return _context.BudgetCategories.Any(e => e.Id == id);
        }
    }
}
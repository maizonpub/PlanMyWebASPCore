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
    public class BlogCategoriesController : ControllerBase
    {
        private readonly DbWebContext _context;

        public BlogCategoriesController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/BlogCategories
        [HttpGet]
        public IEnumerable<BlogCategory> GetBlogCategories()
        {
            return _context.BlogCategories;
        }

        // GET: api/BlogCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogCategory = await _context.BlogCategories.FindAsync(id);

            if (blogCategory == null)
            {
                return NotFound();
            }

            return Ok(blogCategory);
        }

        // PUT: api/BlogCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogCategory([FromRoute] int id, [FromBody] BlogCategory blogCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blogCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(blogCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogCategoryExists(id))
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

        // POST: api/BlogCategories
        [HttpPost]
        public async Task<IActionResult> PostBlogCategory([FromBody] BlogCategory blogCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BlogCategories.Add(blogCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogCategory", new { id = blogCategory.Id }, blogCategory);
        }

        // DELETE: api/BlogCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogCategory = await _context.BlogCategories.FindAsync(id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            _context.BlogCategories.Remove(blogCategory);
            await _context.SaveChangesAsync();

            return Ok(blogCategory);
        }

        private bool BlogCategoryExists(int id)
        {
            return _context.BlogCategories.Any(e => e.Id == id);
        }
    }
}
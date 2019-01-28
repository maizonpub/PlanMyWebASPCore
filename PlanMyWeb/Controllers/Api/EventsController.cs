using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PlanMyWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DbWebContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EventsController(DbWebContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public IEnumerable<Events> GetEvents()
        {
            return _context.Events.Include(x=>x.User);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }
        private async void UploadFile(IFormFile media, string FileName)
        {
            string filePath = _hostingEnvironment.WebRootPath + "/Media/" + FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> PostEvents([FromBody] EventsViewModel events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string filename = "";
            if(events.Image!=null && events.Image.Length>0)
            {
                filename = events.Image.FileName;
                UploadFile(events.Image, filename);
            }
            var row = _context.Events.Where(x => x.UserId == events.UserId).FirstOrDefault();
            if (row == null)
            {
                row = new Events { Date = events.Date, Description = events.Description, Title = events.Title, UserId = events.UserId, Image = filename, IsPrivate = events.IsPrivate };
                _context.Events.Add(row);
            }
            else
            {
                row.Date = events.Date;
                row.Description = events.Description;
                row.Title = events.Title;
                row.Image = filename;
                row.IsPrivate = events.IsPrivate;
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvents", new { id = events.Id }, events);
        }

        

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
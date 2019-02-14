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
        public IEnumerable<Events> GetEvents(string q)
        {
            q = q.ToLower();
            if (!string.IsNullOrEmpty(q))
                return _context.Events.Include(x => x.User).Where(x => x.Title.ToLower().Contains(q) || x.Description.ToLower().Contains(q) && x.IsPrivate !=true);
            else
                return _context.Events.Include(x => x.User).Where(x => x.IsPrivate != true);
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
        public async Task<IActionResult> PostEvents(string UTitle, string UDescription, DateTime UDate, string UUserId, bool UIsPrivate, IFormFile UImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string filename = "";
            if(UImage!=null && UImage.Length>0)
            {
                filename = UImage.FileName;
                UploadFile(UImage, filename);
            }
            var row = _context.Events.Where(x => x.UserId == Request.Form["UUserId"]).FirstOrDefault();
            if (row == null)
            {
                row = new Events { Date = DateTime.Parse(Request.Form["UDate"].ToString()), Description = Request.Form["UDescription"], Title = Request.Form["UTitle"], UserId = Request.Form["UUserId"], Image = filename, IsPrivate = bool.Parse(Request.Form["UIsPrivate"]) };
                _context.Events.Add(row);
            }
            else
            {
                row.Date = DateTime.Parse(Request.Form["UDate"].ToString());
                row.Description = Request.Form["UDescription"];
                row.Title = Request.Form["UTitle"];
                row.Image = filename;
                row.IsPrivate = bool.Parse(Request.Form["UIsPrivate"]);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvents", new { id = row.Id }, row);
        }

        // PUT: api/Offers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvents([FromRoute] int id, [FromBody] Events events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != events.Id)
            {
                return BadRequest();
            }

            _context.Entry(events).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsExists(id))
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

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
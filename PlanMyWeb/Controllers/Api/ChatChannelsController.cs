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
    public class ChatChannelsController : ControllerBase
    {
        private readonly DbWebContext _context;

        public ChatChannelsController(DbWebContext context)
        {
            _context = context;
        }

        // GET: api/ChatChannels
        [HttpGet]
        public IEnumerable<ChatChannel> GetChatChannels(string UserId, string VendorId)
        {
            if(!string.IsNullOrEmpty(VendorId))
                return _context.ChatChannels.Include(x=>x.Vendor).Where(x=>x.UserId == UserId && x.VendorId == VendorId);
            else
                return _context.ChatChannels.Include(x => x.Vendor).Where(x => x.UserId == UserId);
        }

        // GET: api/ChatChannels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChatChannel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chatChannel = await _context.ChatChannels.FindAsync(id);

            if (chatChannel == null)
            {
                return NotFound();
            }

            return Ok(chatChannel);
        }

        // PUT: api/ChatChannels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatChannel([FromRoute] int id, [FromBody] ChatChannel chatChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chatChannel.Id)
            {
                return BadRequest();
            }

            _context.Entry(chatChannel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatChannelExists(id))
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

        // POST: api/ChatChannels
        [HttpPost]
        public async Task<IActionResult> PostChatChannel([FromBody] ChatChannel chatChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ChatChannels.Add(chatChannel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatChannel", new { id = chatChannel.Id }, chatChannel);
        }

        // DELETE: api/ChatChannels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatChannel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chatChannel = await _context.ChatChannels.FindAsync(id);
            if (chatChannel == null)
            {
                return NotFound();
            }

            _context.ChatChannels.Remove(chatChannel);
            await _context.SaveChangesAsync();

            return Ok(chatChannel);
        }

        private bool ChatChannelExists(int id)
        {
            return _context.ChatChannels.Any(e => e.Id == id);
        }
    }
}
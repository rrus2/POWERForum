using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POWERForum.Context;
using POWERForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POWERForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: MessagesController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return Ok(await _context.Messages.ToListAsync());
        }

        // GET: MessagesController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageID == id);

            if (message == null)
                return NotFound();

            return Ok(message);
        }

        // GET: MessagesController/CreateMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Message>> CreateMessage(Message message)
        {
            if (message == null)
                return BadRequest();

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.MessageID }, message);
        }

        // POST: MessagesController/Update/5
        [HttpPut]
        public async Task<ActionResult<Message>> UpdateMessage(int id, Message message)
        {
            if (id != message.MessageID)
                return BadRequest();

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // GET: MessagesController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Message>> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
                return NotFound();

            return Ok(message);
        }

        private bool MessageExists(int id)
        {
            if(_context.Messages.Any(x => x.MessageID == id))
                return true;
            else
                return false;
        }
    }
}

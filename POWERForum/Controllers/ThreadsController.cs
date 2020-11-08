using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POWERForum.Context;
using POWERForum.Models;

namespace POWERForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThreadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Threads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Thread>>> GetThreads()
        {
            return Ok(await _context.Threads.Include(x => x.Blog).ToListAsync());
        }

        // GET: api/Threads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Thread>> GetThread(int id)
        {
            var thread = await _context.Threads.Include(x => x.Blog).FirstOrDefaultAsync(x => x.ID == id);

            if (thread == null)
            {
                return NotFound();
            }

            return Ok(thread);
        }

        // PUT: api/Threads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThread(int id, Thread thread)
        {
            if (id != thread.ID)
            {
                return BadRequest();
            }

            _context.Entry(thread).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThreadExists(id))
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

        // POST: api/Threads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Thread>> PostThread()
        {
            var name = Request.Form["name"][0];
            var type = Request.Form["type"][0];

            var thread = new Thread()
            {
                Name = name,
                Type = type,
            };

            _context.Threads.Add(thread);
            await _context.SaveChangesAsync();
            return new Thread();
        }

        // DELETE: api/Threads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Thread>> DeleteThread(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            if (thread == null)
            {
                return NotFound();
            }

            _context.Threads.Remove(thread);
            await _context.SaveChangesAsync();

            return Ok(thread);
        }

        private bool ThreadExists(int id)
        {
            return _context.Threads.Any(e => e.ID == id);
        }
    }
}

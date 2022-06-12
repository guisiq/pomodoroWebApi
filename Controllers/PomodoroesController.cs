using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pomodoro.data;

namespace pomodoro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PomodoroesController : ControllerBase
    {
        private readonly ApiContext _context;

        public PomodoroesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Pomodoroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pomodoro>>> GetPomodoros()
        {
          if (_context.Pomodoros == null)
          {
              return NotFound();
          }
            return await _context.Pomodoros.ToListAsync();
        }

        // GET: api/Pomodoroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pomodoro>> GetPomodoro(long id)
        {
          if (_context.Pomodoros == null)
          {
              return NotFound();
          }
            var pomodoro = await _context.Pomodoros.FindAsync(id);

            if (pomodoro == null)
            {
                return NotFound();
            }

            return pomodoro;
        }

        // PUT: api/Pomodoroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPomodoro(long id, Pomodoro pomodoro)
        {
            if (id != pomodoro.PomodoroId)
            {
                return BadRequest();
            }

            _context.Entry(pomodoro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PomodoroExists(id))
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

        // POST: api/Pomodoroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pomodoro>> PostPomodoro(Pomodoro pomodoro)
        {
          if (_context.Pomodoros == null)
          {
              return Problem("Entity set 'ApiContext.Pomodoros'  is null.");
          }
            _context.Pomodoros.Add(pomodoro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPomodoro", new { id = pomodoro.PomodoroId }, pomodoro);
        }

        // DELETE: api/Pomodoroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePomodoro(long id)
        {
            if (_context.Pomodoros == null)
            {
                return NotFound();
            }
            var pomodoro = await _context.Pomodoros.FindAsync(id);
            if (pomodoro == null)
            {
                return NotFound();
            }

            _context.Pomodoros.Remove(pomodoro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PomodoroExists(long id)
        {
            return (_context.Pomodoros?.Any(e => e.PomodoroId == id)).GetValueOrDefault();
        }
    }
}

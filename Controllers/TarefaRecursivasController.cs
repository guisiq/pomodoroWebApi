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
    public class TarefaRecursivasController : ControllerBase
    {
        private readonly ApiContext _context;

        public TarefaRecursivasController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/TarefaRecursivas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaRecursiva>>> GetTarefaRecursivas()
        {
          if (_context.TarefaRecursivas == null)
          {
              return NotFound();
          }
            return await _context.TarefaRecursivas.ToListAsync();
        }

        // GET: api/TarefaRecursivas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaRecursiva>> GetTarefaRecursiva(long id)
        {
          if (_context.TarefaRecursivas == null)
          {
              return NotFound();
          }
            var tarefaRecursiva = await _context.TarefaRecursivas.FindAsync(id);

            if (tarefaRecursiva == null)
            {
                return NotFound();
            }

            return tarefaRecursiva;
        }

        // PUT: api/TarefaRecursivas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefaRecursiva(long id, TarefaRecursiva tarefaRecursiva)
        {
            if (id != tarefaRecursiva.TarefaId)
            {
                return BadRequest();
            }

            _context.Entry(tarefaRecursiva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaRecursivaExists(id))
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

        // POST: api/TarefaRecursivas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TarefaRecursiva>> PostTarefaRecursiva(TarefaRecursiva tarefaRecursiva)
        {
          if (_context.TarefaRecursivas == null)
          {
              return Problem("Entity set 'ApiContext.TarefaRecursivas'  is null.");
          }
            _context.TarefaRecursivas.Add(tarefaRecursiva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarefaRecursiva", new { id = tarefaRecursiva.TarefaId }, tarefaRecursiva);
        }

        // DELETE: api/TarefaRecursivas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefaRecursiva(long id)
        {
            if (_context.TarefaRecursivas == null)
            {
                return NotFound();
            }
            var tarefaRecursiva = await _context.TarefaRecursivas.FindAsync(id);
            if (tarefaRecursiva == null)
            {
                return NotFound();
            }

            _context.TarefaRecursivas.Remove(tarefaRecursiva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarefaRecursivaExists(long id)
        {
            return (_context.TarefaRecursivas?.Any(e => e.TarefaId == id)).GetValueOrDefault();
        }
    }
}

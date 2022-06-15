using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pomodoro.data;

namespace pomodoro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        private readonly ApiContext _context;

        public PomodoroController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Pomodoroes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Pomodoro>>> GetPomodoros([FromQuery] int? tarefaID)
        {
          if (_context.Pomodoros == null)
          {
              return NotFound();
          }
           var pomodorosQuery = _context?.Usuarios
                                ?.Where(x => x.Login == User.Identity.Name)
                                ?.SelectMany(x => x.Metas)
                                ?.SelectMany(x => x.Tarefas)
                                ?.Where(x => tarefaID == null || x.TarefaId == tarefaID)
                                ?.SelectMany(x => x.Pomodoros);
            return await _context.Pomodoros.ToListAsync();
        }

        // GET: api/Pomodoroes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Pomodoro>> GetPomodoro(long id)
        {
          if (_context.Pomodoros == null)
          {
              return NotFound();
          }
            var pomodoro = await _context?.Usuarios
                                ?.Where(x => x.Login == User.Identity.Name)
                                ?.SelectMany(x => x.Metas)
                                ?.SelectMany(x => x.Tarefas)
                                ?.SelectMany(x => x.Pomodoros)
                                ?.Where(x => x.PomodoroId == id)
                                ?.FirstAsync();
            if (pomodoro == null)
            {
                if(PomodoroExists(id)){
                    return Unauthorized();
                }
                return NotFound();
            }

            return pomodoro;
        }

        // PUT: api/Pomodoroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPomodoro(long id, Pomodoro pomodoro)
        {
            if (id != pomodoro.PomodoroId)
            {
                return BadRequest();
            }
            var pomodoroDb = await _context?.Usuarios
                    ?.Where(x => x.Login == User.Identity.Name)
                    ?.SelectMany(x => x.Metas)
                    ?.SelectMany(x => x.Tarefas)
                    ?.SelectMany(x => x.Pomodoros)
                    ?.Where(x => x.PomodoroId == id)
                    ?.FirstAsync();
            if (pomodoroDb == null)
            {
                if(PomodoroExists(id)){
                    return Unauthorized();
                }
                return NotFound();
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
        [HttpPost("{tarefaID}")]
        [Authorize]
        public async Task<ActionResult<Pomodoro>> PostPomodoro(Pomodoro pomodoro,long tarefaID)
        {
            if (_context.Pomodoros == null)
            {
                return Problem("Entity set 'ApiContext.Pomodoros'  is null.");
            }
            var tarefa = await _context?.Usuarios
                    ?.Where(x => x.Login == User.Identity.Name)
                    ?.SelectMany(x => x.Metas)
                    ?.SelectMany(x => x.Tarefas)
                    ?.Where(x => x.TarefaId == tarefaID)
                    ?.FirstAsync();
            if (tarefa == null)
            {
                if(_context.Tarefas.Any(x => x.TarefaId == tarefaID)){
                    return Unauthorized();
                }
                return NotFound();
            }
            _context.Pomodoros.Add(pomodoro);
            tarefa.Pomodoros.Add(pomodoro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPomodoro", new { id = pomodoro.PomodoroId }, pomodoro);
        }

        // DELETE: api/Pomodoroes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePomodoro(long id)
        {
            if (_context.Pomodoros == null)
            {
                return NotFound();
            }
            var pomodoro = await _context.Pomodoros.FindAsync(id);
            var pomodoroDb = await _context?.Usuarios
                    ?.Where(x => x.Login == User.Identity.Name)
                    ?.SelectMany(x => x.Metas)
                    ?.SelectMany(x => x.Tarefas)
                    ?.SelectMany(x => x.Pomodoros)
                    ?.Where(x => x.PomodoroId == id)
                    ?.FirstAsync();
            if(pomodoroDb is null&& pomodoro is not null){
                return Unauthorized();
            }
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

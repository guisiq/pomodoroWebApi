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
    public class TarefasController : ControllerBase
    {
        private readonly ApiContext _context;

        public TarefasController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Tarefas
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas([FromQuery] int? metaID)
        {
            if (_context?.Usuarios == null)
            {
                return NotFound();
            }
            //opcao 1
            // var tarefas = _context?.Metas
            //                     ?.Where(x => x.Usuarios.Any(x => x.Login == User.Identity.Name))
            //                     ?.SelectMany(x=>x.Tarefas)
            //                     ?.ToList();
            //opcao2
            var tarefasQuery = _context?.Usuarios
                                ?.Where(x => x.Login == User.Identity.Name)
                                ?.SelectMany(x => x.Metas)
                                ?.Where(x => metaID == null || x.MetasId == metaID)
                                ?.SelectMany(x => x.Tarefas);
            if (tarefasQuery is not null)
            {
                tarefasQuery.ToString();
                return tarefasQuery.ToList();
            }
            return NoContent();
        }

        // GET: api/Tarefas/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Tarefa>> GetTarefa(long id)
        {
            if (_context.Tarefas == null)
            {
                return NotFound();
            }
            var tarefa =  _context?.Usuarios
                                ?.Where(x => x.Login == User.Identity.Name)
                                ?.SelectMany(x => x.Metas)
                                ?.SelectMany(x => x.Tarefas)
                                ?.Where(x => x.TarefaId == id)
                                ?.Single();

            if (tarefa == null)
            {
                if(TarefaExists(id)){
                    return Unauthorized();
                }
                return NotFound();
            }

            return tarefa;
        }

        // PUT: api/Tarefas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTarefa(long id, Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                return BadRequest();
            }
            
            _context.Entry(tarefa).State = EntityState.Modified;
            
            
            try
            {
                var tarefadb =  _context?.Usuarios
                                ?.Where(x => x.Login == User.Identity.Name)
                                ?.SelectMany(x => x.Metas)
                                ?.SelectMany(x => x.Tarefas)
                                ?.Where(x => x.TarefaId == id)
                                ?.Single();
                if(tarefadb is null && TarefaExists(id)){
                    return Unauthorized();
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
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

        // POST: api/Tarefas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{metaId}")]
        public async Task<ActionResult<Tarefa>> PostTarefa(long metaId ,Tarefa tarefa)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'ApiContext.Tarefas'  is null.");
            }

            var meta = _context.Metas.Where(x => x.MetasId == metaId && x.Usuarios.Any(x => x.Login == User.Identity.Name)).First();
            if(meta is null &&(_context.Metas?.Any(e => e.MetasId == metaId)).GetValueOrDefault()){
                return Unauthorized();
            }
            if(meta.Tarefas == null){
                meta.Tarefas = new List<Tarefa>();
            }
            meta.Tarefas.Add(tarefa);
            //_context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarefa", new { id = tarefa.TarefaId }, tarefa);
        }

        // DELETE: api/Tarefas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(long id)
        {
            if (_context.Tarefas == null)
            {
                return NotFound();
            }
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
             var tarefadb =  _context.Usuarios
                                ?.Where(x => x.Login == User.Identity.Name)
                                ?.SelectMany(x => x.Metas)
                                ?.SelectMany(x => x.Tarefas)
                                ?.Where(x => x.TarefaId == id)
                                ?.Single();
            if(tarefadb is null && TarefaExists(id)){
                return Unauthorized();
            }else{
                _context.Tarefas.Remove(tarefa);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarefaExists(long id)
        {
            return (_context.Tarefas?.Any(e => e.TarefaId == id)).GetValueOrDefault();
        }
    }
}

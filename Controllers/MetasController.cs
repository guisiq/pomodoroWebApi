using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pomodoro.data;

namespace pomodoro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetasController : ControllerBase
    {
        private readonly ApiContext _context;

        public MetasController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Metas
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetMetas()
        {
            if (_context.Metas == null)
            {
                return NotFound();
            }
            //var userLog = await _context?.Usuarios?.Where(x => x.Login == User.Identity.Name ).FirstAsync();
            //var userLog = await _context?.Usuarios?.Where(x => x.Login == User.Identity.Name )?.Select(x => new Usuario(x.Nome,x.Login,"",x.Role){Metas = x.Metas})?.FirstAsync();

            //return  userLog.Metas.ToList();
            return  _context.Usuarios
                                ?.Where(x => x.Login == User.Identity.Name)
                                ?.SelectMany(x => x.Metas)
                                ?.Select(x => new
                                {
                                    x.MetasId,
                                    x.Descricao,
                                    x.Tarefas,
                                    Usuarios = x.Usuarios.Select(x => new{ x.Nome , x.UsuarioId})
                                })
                                ?.ToList();
             
                               
        }

        // GET: api/Metas/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetMeta(long id)
        {
            if (_context?.Usuarios is null)
            {
                return NotFound();
            }
            var meta = _context?.Usuarios
                                    ?.Where(x => x.Login == User.Identity.Name)
                                    ?.Select(x => x.Metas)
                                    ?.FirstOrDefault()
                                    ?.Where(x => x.MetasId == id)
                                    ?.Select(x => new
                                    {
                                        x.Descricao,
                                        x.MetasId,
                                        tarefasIds = x?.Tarefas?.Select(x => x.TarefaId),
                                        usuariosIds = x?.Usuarios?.Select(x => x.UsuarioId)
                                    })
                                    ?.FirstOrDefault();
            if (meta == null)
            {
                return NotFound();
            }
            else
            {
                return meta;
            }
        }

        // PUT: api/Metas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutMeta(long id, Meta meta)
        {
            if (id != meta.MetasId)
            {
                return BadRequest();
            }
            //Obtém um microsoft.entityframeworkcore.changeTracking.entityEntry <Tentity> para a entidade dada.A entrada fornece acesso para alterar informações e operações de rastreamento para a entidade.
            _context.Entry(meta).State = EntityState.Modified;

            try
            {
                var isEditavel = _context?.Usuarios
                                            ?.Where(x => x.Login == User.Identity.Name)
                                            ?.Select(x => x.Metas)
                                            ?.First()
                                            ?.Any(x => x.MetasId == meta.MetasId) ?? false;
                if (isEditavel)
                {
                    await _context.SaveChangesAsync();
                }
                else if (MetaExists(id))
                {
                    return Unauthorized();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaExists(id))
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

        // POST: api/Metas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Meta>> PostMeta(Meta meta)
        {
            if (_context?.Metas is null)
            {
                return Problem("Entity set 'ApiContext.Metas'  is null.");
            }
            if (_context?.Usuarios is null)
            {
                return Problem("Entity set 'ApiContext.Metas'  is null.");
            }
            if (User?.Identity is null)
            {
                return Unauthorized();
            }
            var userLog = _context.Usuarios.Where(x => x.Login == User.Identity.Name).FirstOrDefault();
            if (userLog is null)
            {
                return Unauthorized();
            }
            meta.Usuarios = new List<Usuario>() { userLog };
            _context.Metas.Add(meta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeta", new { id = meta.MetasId }, meta);


        }

        // DELETE: api/Metas/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMeta(long id)
        {
            if (_context.Metas == null)
            {
                return NotFound();
            }
            var isExcluivel = _context?.Usuarios
                                        ?.Where(x => x.Login == User.Identity.Name)
                                        ?.Select(x => x.Metas)
                                        ?.First()
                                        ?.Any(x => x.MetasId == id) ?? false;
            if (isExcluivel)
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            else if (MetaExists(id))
            {
                return Unauthorized();
            }
            else
            {
                return NotFound();
            }
        }

        private bool MetaExists(long id)
        {
            return (_context.Metas?.Any(e => e.MetasId == id)).GetValueOrDefault();
        }
    }
}

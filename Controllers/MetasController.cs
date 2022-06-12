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
        public async Task<ActionResult<IEnumerable<Meta>>> GetMetas()
        {
          if (_context.Metas == null)
          {
              return NotFound();
          }
            return await _context.Metas.ToListAsync();
        }

        // GET: api/Metas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meta>> GetMeta(long id)
        {
          if (_context.Metas == null)
          {
              return NotFound();
          }
            var meta = await _context.Metas.FindAsync(id);

            if (meta == null)
            {
                return NotFound();
            }

            return meta;
        }

        // PUT: api/Metas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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
                await _context.SaveChangesAsync();
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
            if (_context.Metas == null)
            {
                return Problem("Entity set 'ApiContext.Metas'  is null.");
            }
            var userLog = _context.Usuarios.Where(x => x.Login == User.Identity.Name ).FirstOrDefault();
            _context.Metas.Add(meta);
            userLog?.Metas.Add(meta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeta", new { id = meta.MetasId }, meta);
                
          
        }

        // DELETE: api/Metas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeta(long id)
        {
            if (_context.Metas == null)
            {
                return NotFound();
            }
            var meta = await _context.Metas.FindAsync(id);
            if (meta == null)
            {
                return NotFound();
            }

            _context.Metas.Remove(meta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetaExists(long id)
        {
            return (_context.Metas?.Any(e => e.MetasId == id)).GetValueOrDefault();
        }
    }
}

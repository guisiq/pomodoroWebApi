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
        public async Task<ActionResult<IEnumerable<Meta>>> GetMetas()
        {
            if (_context.Metas == null)
            {
                return NotFound();
            }
            var userLog = await _context?.Usuarios?.Where(x => x.Login == User.Identity.Name ).FirstAsync();
            return  userLog.Metas.ToList();
        }

        // GET: api/Metas/5
        [HttpGet("{id}")]
        [Authorize]
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
            var userLog = _context?.Usuarios?.Where(x => x.Login == User.Identity.Name ).FirstOrDefault();
            if(userLog?.Metas.ToList().Exists(x => x.MetasId == meta.MetasId)??false){
                return meta;
            }else{
                return Unauthorized();
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
                var userLog = _context?.Usuarios?.Where(x => x.Login == User.Identity.Name ).FirstOrDefault();
                if(userLog?.Metas.ToList().Exists(x => x.MetasId == meta.MetasId)??false){
                    await _context.SaveChangesAsync();
                }else{
                    return Unauthorized();
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
            if(User?.Identity is null){
                return Unauthorized();
            }
            var userLog = _context.Usuarios.Where(x => x.Login == User.Identity.Name ).FirstOrDefault();
            if(userLog is null){
                return Unauthorized();
            }
            _context.Metas.Add(meta);
            if(userLog.Metas is null){
                userLog.Metas = new List<Meta>();
            }
            userLog?.Metas.Add(meta);
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
            var meta = await _context.Metas.FindAsync(id);
            if (meta == null)
            {
                return NotFound();
            }
            var userLog = _context?.Usuarios?.Where(x => x.Login == User.Identity.Name ).FirstOrDefault();
            if(userLog?.Metas.ToList().Exists(x => x.MetasId == meta.MetasId)??false){
                _context.Metas.Remove(meta);
                await _context.SaveChangesAsync();

                return NoContent();
            }else{
                return Unauthorized();
            }
        }

        private bool MetaExists(long id)
        {
            return (_context.Metas?.Any(e => e.MetasId == id)).GetValueOrDefault();
        }
    }
}

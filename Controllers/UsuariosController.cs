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
    public class UsuariosController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsuariosController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        // {
        //   if (_context.Usuarios == null)
        //   {
        //       return NotFound();
        //   }
        //     return await _context.Usuarios.ToListAsync();
        // }

        // // GET: api/Usuarios/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Usuario>> GetUsuario(long id)
        // {
        //   if (_context.Usuarios == null)
        //   {
        //       return NotFound();
        //   }
        //     var usuario = await _context.Usuarios.FindAsync(id);

        //     if (usuario == null)
        //     {
        //         return NotFound();
        //     }

        //     return usuario;
        // }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsuario(long id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }
                    
            if(!(_context.Usuarios?.Any(x => x.Login == User.Identity.Name)??false)){
                return Unauthorized();
            }
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'ApiContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            if(!(_context.Usuarios?.Any(x => x.Login == User.Identity.Name && x.UsuarioId == id)??false)){
                return Unauthorized();
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(long id)
        {
            return (_context.Usuarios?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }
    }
}

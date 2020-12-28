using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DIO_Armazen.Models;

namespace Armazen_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilitariosController : ControllerBase
    {
        private readonly Context _context;

        public UtilitariosController(Context context)
        {
            _context = context;
        }

        // GET: api/Utilitarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilitario>>> GetUtilitarios()
        {
            return await _context.Utilitarios.ToListAsync();
        }

        // GET: api/Utilitarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilitario>> GetUtilitario(int id)
        {
            var utilitario = await _context.Utilitarios.FindAsync(id);

            if (utilitario == null)
            {
                return NotFound();
            }

            return utilitario;
        }

        // PUT: api/Utilitarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilitario(int id, Utilitario utilitario)
        {
            if (id != utilitario.id)
            {
                return BadRequest();
            }

            _context.Entry(utilitario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilitarioExists(id))
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

        // POST: api/Utilitarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilitario>> PostUtilitario(Utilitario utilitario)
        {
            _context.Utilitarios.Add(utilitario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilitario", new { id = utilitario.id }, utilitario);
        }

        // DELETE: api/Utilitarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilitario(int id)
        {
            var utilitario = await _context.Utilitarios.FindAsync(id);
            if (utilitario == null)
            {
                return NotFound();
            }

            _context.Utilitarios.Remove(utilitario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilitarioExists(int id)
        {
            return _context.Utilitarios.Any(e => e.id == id);
        }
    }
}

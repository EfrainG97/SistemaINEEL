using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibreriaModelos;
using SistemaAPI.Data;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SistemaController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Sistemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sistema>>> GetSistema()
        {
            return await _context.Sistema.ToListAsync();
        }

        // GET: api/Sistemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sistema>> GetSistema(int id)
        {
            var sistema = await _context.Sistema.FindAsync(id);

            if (sistema == null)
            {
                return NotFound();
            }

            return sistema;
        }

        // PUT: api/Sistemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSistema(int id, Sistema sistema)
        {
            if (id != sistema.SistemaID)
            {
                return BadRequest();
            }

            _context.Entry(sistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SistemaExists(id))
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

        // POST: api/Sistemas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sistema>> PostSistema(Sistema sistema)
        {
            _context.Sistema.Add(sistema);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSistema", new { id = sistema.SistemaID }, sistema);
        }

        // DELETE: api/Sistemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSistema(int id)
        {
            var sistema = await _context.Sistema.FindAsync(id);
            if (sistema == null)
            {
                return NotFound();
            }

            _context.Sistema.Remove(sistema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SistemaExists(int id)
        {
            return _context.Sistema.Any(e => e.SistemaID == id);
        }
    }
}

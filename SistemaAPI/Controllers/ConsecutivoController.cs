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
    public class ConsecutivoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ConsecutivoController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Consecutivos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consecutivo>>> GetConsecutivo()
        {
            return await _context.Consecutivo.ToListAsync();
        }

        // GET: api/Consecutivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consecutivo>> GetConsecutivo(int id)
        {
            var consecutivo = await _context.Consecutivo.FindAsync(id);

            if (consecutivo == null)
            {
                return NotFound();
            }

            return consecutivo;
        }

        // PUT: api/Consecutivos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsecutivo(int id, Consecutivo consecutivo)
        {
            if (id != consecutivo.ConsecutivoID)
            {
                return BadRequest();
            }

            _context.Entry(consecutivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsecutivoExists(id))
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

        // POST: api/Consecutivos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consecutivo>> PostConsecutivo(Consecutivo consecutivo)
        {
            _context.Consecutivo.Add(consecutivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsecutivo", new { id = consecutivo.ConsecutivoID }, consecutivo);
        }

        // DELETE: api/Consecutivos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsecutivo(int id)
        {
            var consecutivo = await _context.Consecutivo.FindAsync(id);
            if (consecutivo == null)
            {
                return NotFound();
            }

            _context.Consecutivo.Remove(consecutivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsecutivoExists(int id)
        {
            return _context.Consecutivo.Any(e => e.ConsecutivoID == id);
        }
    }
}

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
    public class ReporteController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ReporteController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Reporte
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reporte>>> GetReporte()
        {
            return await _context.Reporte.ToListAsync();
        }

        // GET: api/Reporte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reporte>> GetReporte(int id)
        {
            var reporte = await _context.Reporte.FindAsync(id);

            if (reporte == null)
            {
                return NotFound();
            }

            return reporte;
        }

        // PUT: api/Reporte/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReporte(int id, Reporte reporte)
        {
            if (id != reporte.ReporteID)
            {
                return BadRequest();
            }

            _context.Entry(reporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReporteExists(id))
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

        // POST: api/Reporte
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reporte>> PostReporte(Reporte reporte)
        {
            _context.Reporte.Add(reporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReporte", new { id = reporte.ReporteID }, reporte);
        }

        // DELETE: api/Reporte/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReporte(int id)
        {
            var reporte = await _context.Reporte.FindAsync(id);
            if (reporte == null)
            {
                return NotFound();
            }

            _context.Reporte.Remove(reporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReporteExists(int id)
        {
            return _context.Reporte.Any(e => e.ReporteID == id);
        }
    }
}

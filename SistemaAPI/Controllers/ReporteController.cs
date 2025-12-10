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
        #region Campos
        private readonly AppDBContext _context;
        #endregion

        #region Constructor
        public ReporteController(AppDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Métodos GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reporte>>> GetReporte()
        {
            return await _context.Reporte.ToListAsync();
        }

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
        #endregion

        #region Métodos PUT
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
        #endregion

        #region Métodos POST
        [HttpPost]
        public async Task<ActionResult<Reporte>> PostReporte(Reporte reporte)
        {
            _context.Reporte.Add(reporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReporte", new { id = reporte.ReporteID }, reporte);
        }
        #endregion

        #region Métodos DELETE
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
        #endregion

        #region Métodos Privados
        private bool ReporteExists(int id)
        {
            return _context.Reporte.Any(e => e.ReporteID == id);
        }
        #endregion
    }
}

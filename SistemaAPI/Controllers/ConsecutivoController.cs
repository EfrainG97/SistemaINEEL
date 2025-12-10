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
        #region Campos
        private readonly AppDBContext _context;
        #endregion

        #region Constructor
        public ConsecutivoController(AppDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Métodos GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consecutivo>>> GetConsecutivo()
        {
            return await _context.Consecutivo.Where(c => c.Activo).ToListAsync();
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Consecutivo>>> GetAllConsecutivos()
        {
            return await _context.Consecutivo.ToListAsync();
        }

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
        #endregion

        #region Métodos PUT
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
        #endregion

        #region Métodos POST
        [HttpPost]
        public async Task<ActionResult<Consecutivo>> PostConsecutivo(Consecutivo consecutivo)
        {
            _context.Consecutivo.Add(consecutivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsecutivo", new { id = consecutivo.ConsecutivoID }, consecutivo);
        }
        #endregion

        #region Métodos DELETE
        /// <summary>
        /// Realiza un borrado lógico del consecutivo marcándolo como inactivo en lugar de eliminarlo físicamente.
        /// Esto permite mantener el historial y evitar problemas de integridad referencial.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsecutivo(int id)
        {
            var consecutivo = await _context.Consecutivo.FindAsync(id);
            if (consecutivo == null)
            {
                return NotFound();
            }

            consecutivo.Activo = false;
            _context.Entry(consecutivo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Métodos Privados
        private bool ConsecutivoExists(int id)
        {
            return _context.Consecutivo.Any(e => e.ConsecutivoID == id);
        }
        #endregion
    }
}

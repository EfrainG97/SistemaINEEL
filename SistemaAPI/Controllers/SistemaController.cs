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
        #region Campos
        private readonly AppDBContext _context;
        #endregion

        #region Constructor
        public SistemaController(AppDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Métodos GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sistema>>> GetSistema()
        {
            return await _context.Sistema.ToListAsync();
        }

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
        #endregion

        #region Métodos PUT
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
        #endregion

        #region Métodos POST
        [HttpPost]
        public async Task<ActionResult<Sistema>> PostSistema(Sistema sistema)
        {
            _context.Sistema.Add(sistema);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSistema", new { id = sistema.SistemaID }, sistema);
        }
        #endregion

        #region Métodos DELETE
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
        #endregion

        #region Métodos Privados
        private bool SistemaExists(int id)
        {
            return _context.Sistema.Any(e => e.SistemaID == id);
        }
        #endregion
    }
}

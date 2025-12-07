using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaINEEL.Data;
using SistemaINEEL.Filters;
using SistemaINEEL.Models;

namespace SistemaINEEL.Controllers
{
    [SessionFilter]
    public class SistemaController : Controller
    {
        private readonly AppDBContext _context;

        public SistemaController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Config()
        {
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> EditarGerencia(string nuevaGerencia)
        {
            int sistemaId = 1;
            var sistema = await _context.Sistema.FindAsync(sistemaId);
            if (sistema == null)
            {
                return NotFound();
            }
            sistema.Gerencia = nuevaGerencia;
            await _context.SaveChangesAsync();
            return RedirectToAction("Config");
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuarios(int Num, string User, string Pass, int Rol)
        {
            var nuevoUsuario = new Usuario
            {
                NumEmpleado = Num,
                NombreUsuario = User,
                Password = Pass,
                IDRol = Rol
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Config");
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Config");
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(int usuarioId, int Num, string User, string Pass, int Rol)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.NumEmpleado = Num;
            usuario.NombreUsuario = User;
            usuario.Password = Pass;
            usuario.IDRol = Rol;
            await _context.SaveChangesAsync();
            return RedirectToAction("Config");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Json(usuarios);
        }
    }
}

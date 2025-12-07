using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaINEEL.Data;
using SistemaINEEL.Models;
using SistemaINEEL.ViewModels;
using System.Diagnostics;

namespace SistemaINEEL.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDBContext _context;

        public LoginController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioLogeado") == "true")
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", modelo);
            }

            var usuario = await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.NumEmpleado == modelo.NumEmpleado && u.Password == modelo.Password);

            if (usuario != null)
            {
                // Establecer la sesión
                HttpContext.Session.SetString("UsuarioLogeado", "true");
                HttpContext.Session.SetString("UsuarioID", usuario.UsuarioID.ToString());
                HttpContext.Session.SetString("NumEmpleado", usuario.NumEmpleado.ToString());
                HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario ?? "Usuario");

                return RedirectToAction("Index", "Home");
            }

            // Login fallido
            modelo.ErrorMessage = "Número de empleado o contraseña incorrectos";
            return View("Index", modelo);
        }

        [HttpPut]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null || nuevoUsuario.NumEmpleado <= 0 || string.IsNullOrEmpty(nuevoUsuario.Password))
            {
                return BadRequest(new { success = false, message = "Datos de usuario inválidos" });
            }
            // Verificar si el número de empleado ya existe
            var usuarioExistente = await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.NumEmpleado == nuevoUsuario.NumEmpleado);
            if (usuarioExistente != null)
            {
                return Conflict(new { success = false, message = "El número de empleado ya existe" });
            }
            // Agregar el nuevo usuario a la base de datos
            _context.Set<Usuario>().Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Usuario creado exitosamente" });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.UsuarioID == id);
            if (usuario == null)
            {
                return NotFound(new { success = false, message = "Usuario no encontrado" });
            }
            _context.Set<Usuario>().Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Usuario eliminado exitosamente" });
        }

        public IActionResult Logout()
        {
            // Limpiar la sesión
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public async Task<JsonResult> Buscar(int id)
        {
            var usuario = await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.UsuarioID == id);

            if (usuario != null)
            {
                return Json(new { success = true, data = usuario });
            }

            return Json(new { success = false, message = "Usuario no encontrado" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

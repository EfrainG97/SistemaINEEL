using Microsoft.AspNetCore.Mvc;
using SistemaINEEL.ViewModels;
using SistemaINEEL.Models;
using ServiciosAPI.Interfaces;
using LibreriaModelos;
using System.Diagnostics;

namespace SistemaINEEL.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRolService _rolService;

        public LoginController(IUsuarioService usuarioService, IRolService rolService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
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

            try
            {
                var usuarios = await _usuarioService.GetUsuariosAsync();
                var usuario = usuarios.FirstOrDefault(u => u.NumEmpleado == modelo.NumEmpleado && u.Password == modelo.Password);

                if (usuario != null)
                {
                    var rol = await _rolService.GetRolByIdAsync(usuario.IDRol);
                    var nombreRol = rol?.NombreRol?.Trim().ToLowerInvariant() ?? "usuario";

                    HttpContext.Session.SetString("UsuarioLogeado", "true");
                    HttpContext.Session.SetString("UsuarioID", usuario.UsuarioID.ToString());
                    HttpContext.Session.SetString("NumEmpleado", usuario.NumEmpleado.ToString());
                    HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario ?? "Usuario");
                    HttpContext.Session.SetString("NombreRol", nombreRol);

                    return RedirectToAction("Index", "Home");
                }

                modelo.ErrorMessage = "Número de empleado o contraseña incorrectos";
                return View("Index", modelo);
            }
            catch (Exception ex)
            {
                modelo.ErrorMessage = "Error al conectar con el servidor. Por favor, intente más tarde.";
                return View("Index", modelo);
            }
        }

        [HttpPut]
        public async Task<IActionResult> CrearUsuario([FromBody] LibreriaModelos.Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null || nuevoUsuario.NumEmpleado <= 0 || string.IsNullOrEmpty(nuevoUsuario.Password))
            {
                return BadRequest(new { success = false, message = "Datos de usuario inválidos" });
            }
            try
            {
                var usuarios = await _usuarioService.GetUsuariosAsync();
                var usuarioExistente = usuarios.FirstOrDefault(u => u.NumEmpleado == nuevoUsuario.NumEmpleado);
                if (usuarioExistente != null)
                {
                    return Conflict(new { success = false, message = "El número de empleado ya existe" });
                }
                await _usuarioService.PostUsuarioAsync(nuevoUsuario);
                return Ok(new { success = true, message = "Usuario creado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error al crear el usuario: " + ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound(new { success = false, message = "Usuario no encontrado" });
                }
                await _usuarioService.DeleteUsuarioAsync(id);
                return Ok(new { success = true, message = "Usuario eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error al eliminar el usuario: " + ex.Message });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public async Task<JsonResult> Buscar(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                if (usuario != null)
                {
                    return Json(new { success = true, data = usuario });
                }
                return Json(new { success = false, message = "Usuario no encontrado" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al buscar el usuario: " + ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

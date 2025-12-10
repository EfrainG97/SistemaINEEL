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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

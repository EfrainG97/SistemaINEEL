using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaINEEL.Filters;
using SistemaINEEL.ViewModels;
using SistemaINEEL.Models;
using ServiciosAPI.Interfaces;
using LibreriaModelos;

namespace SistemaINEEL.Controllers
{
    [SessionFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IRolService _rolService;

        public HomeController(ILogger<HomeController> logger, IUsuarioService usuarioService, IRolService rolService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
            _rolService = rolService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                NombreUsuario = HttpContext.Session.GetString("NombreUsuario") ?? "Usuario",
                NombreRol = "usuario"
            };

            var usuarioId = HttpContext.Session.GetString("UsuarioID");
            
            if (!string.IsNullOrEmpty(usuarioId) && int.TryParse(usuarioId, out int userId))
            {
                viewModel.UsuarioId = userId;
                
                try
                {
                    var usuario = await _usuarioService.GetUsuarioByIdAsync(userId);
                    if (usuario != null)
                    {
                        var rol = await _rolService.GetRolByIdAsync(usuario.IDRol);
                        if (rol != null && !string.IsNullOrEmpty(rol.NombreRol))
                        {
                            viewModel.NombreRol = rol.NombreRol.Trim().ToLowerInvariant();
                            HttpContext.Session.SetString("NombreRol", viewModel.NombreRol);
                        }
                        else
                        {
                            viewModel.NombreRol = "usuario";
                            HttpContext.Session.SetString("NombreRol", "usuario");
                        }
                    }
                    else
                    {
                        viewModel.NombreRol = "usuario";
                        HttpContext.Session.SetString("NombreRol", "usuario");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener información del usuario {UserId}: {Message}", userId, ex.Message);
                    var rolSesion = HttpContext.Session.GetString("NombreRol");
                    if (!string.IsNullOrEmpty(rolSesion))
                    {
                        viewModel.NombreRol = rolSesion.Trim().ToLowerInvariant();
                    }
                    else
                    {
                        viewModel.NombreRol = "usuario";
                    }
                }
            }
            else
            {
                var rolSesion = HttpContext.Session.GetString("NombreRol");
                if (!string.IsNullOrEmpty(rolSesion))
                {
                    viewModel.NombreRol = rolSesion.Trim().ToLowerInvariant();
                }
                else
                {
                    viewModel.NombreRol = "usuario";
                }
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

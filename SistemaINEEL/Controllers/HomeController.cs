using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaINEEL.Data;
using SistemaINEEL.Filters;
using SistemaINEEL.Models;
using SistemaINEEL.ViewModels;

namespace SistemaINEEL.Controllers
{
    [SessionFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _context;

        public HomeController(ILogger<HomeController> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var viewModel = new DashboardViewModel
            {
                NombreUsuario = HttpContext.Session.GetString("NombreUsuario") ?? "Usuario"
            };

            var usuarioId = HttpContext.Session.GetString("UsuarioID");
            if (!string.IsNullOrEmpty(usuarioId) && int.TryParse(usuarioId, out int userId))
            {
                viewModel.UsuarioId = userId;
                
                var usuario = await _context.Set<Usuario>()
                    .FirstOrDefaultAsync(u => u.UsuarioID == userId);

                if (usuario != null)
                {
                    var rol = await _context.Set<Rol>()
                        .FirstOrDefaultAsync(r => r.RolID == usuario.IDRol);

                    viewModel.NombreRol = rol?.NombreRol?.ToLower() ?? "usuario";
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

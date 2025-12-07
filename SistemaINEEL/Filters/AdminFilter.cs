using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiciosAPI.Interfaces;
using System.Threading.Tasks;

namespace SistemaINEEL.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var session = context.HttpContext.Session;

            // Validar sesión de usuario
            var usuarioLogueado = session.GetString("UsuarioLogeado")?.Trim().ToLowerInvariant();
            if (usuarioLogueado != "true")
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            // Si ya tenemos el rol en sesión, evitar consultas innecesarias
            var rolNombreEnSesion = session.GetString("RolNombre")?.Trim().ToLowerInvariant();
            if (rolNombreEnSesion == "admin")
            {
                await next();
                return;
            }

            // Validar UsuarioID
            var usuarioIdStr = session.GetString("UsuarioID");
            if (string.IsNullOrWhiteSpace(usuarioIdStr) || !int.TryParse(usuarioIdStr, out int usuarioId))
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }

            var serviceProvider = context.HttpContext.RequestServices;
            var usuarioService = serviceProvider.GetRequiredService<IUsuarioService>();
            var rolService = serviceProvider.GetRequiredService<IRolService>();

            try
            {
                // Consultar usuario (sin tracking para mejorar rendimiento)
                var usuario = await usuarioService.GetUsuarioByIdAsync(usuarioId);
                if (usuario == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                    return;
                }

                // Consultar rol solo si no está en sesión
                var rol = await rolService.GetRolByIdAsync(usuario.IDRol);
                var nombreRol = rol?.NombreRol?.Trim().ToLowerInvariant() ?? string.Empty;

                // Guardar el nombre del rol en sesión para futuras validaciones
                if (!string.IsNullOrEmpty(nombreRol))
                {
                    session.SetString("RolNombre", nombreRol);
                }

                if (rol == null || nombreRol != "admin")
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                    return;
                }
            }
            catch
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }

            await next();
        }
    }
}

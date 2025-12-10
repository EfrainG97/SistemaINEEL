using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiciosAPI.Interfaces;
using System.Threading.Tasks;

namespace SistemaINEEL.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        #region Métodos Override
        /// <summary>
        /// Valida que el usuario tenga permisos de administrador antes de permitir el acceso a la acción.
        /// Verifica primero la sesión, luego consulta la base de datos si es necesario para confirmar el rol.
        /// Redirige al login o al home según corresponda si no tiene permisos de administrador.
        /// </summary>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var session = context.HttpContext.Session;

            var usuarioLogueado = session.GetString("UsuarioLogeado")?.Trim().ToLowerInvariant();
            if (usuarioLogueado != "true")
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }
            var rolNombreEnSesion = session.GetString("RolNombre")?.Trim().ToLowerInvariant();
            if (rolNombreEnSesion == "admin")
            {
                await next();
                return;
            }

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
                var usuario = await usuarioService.GetUsuarioByIdAsync(usuarioId);
                if (usuario == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                    return;
                }

                var rol = await rolService.GetRolByIdAsync(usuario.IDRol);
                var nombreRol = rol?.NombreRol?.Trim().ToLowerInvariant() ?? string.Empty;

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
        #endregion
    }
}

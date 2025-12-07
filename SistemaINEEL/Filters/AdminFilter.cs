using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiciosAPI.Interfaces;

namespace SistemaINEEL.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var usuarioLogueado = session.GetString("UsuarioLogeado");

            if (usuarioLogueado != "true")
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            var usuarioId = session.GetString("UsuarioID");
            if (string.IsNullOrEmpty(usuarioId) || !int.TryParse(usuarioId, out int userId))
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }

            var serviceProvider = context.HttpContext.RequestServices;
            var usuarioService = serviceProvider.GetRequiredService<IUsuarioService>();
            var rolService = serviceProvider.GetRequiredService<IRolService>();

            try
            {
                var usuario = await usuarioService.GetUsuarioByIdAsync(userId);
                if (usuario == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                    return;
                }

                var rol = await rolService.GetRolByIdAsync(usuario.IDRol);
                var nombreRol = rol?.NombreRol?.Trim().ToLowerInvariant() ?? "";
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

            base.OnActionExecuting(context);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SistemaINEEL.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        #region Métodos Override
        /// <summary>
        /// Valida que el usuario tenga una sesión activa antes de permitir el acceso a la acción.
        /// Redirige al login si no hay sesión válida.
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var usuarioLogueado = session.GetString("UsuarioLogeado");

            if (usuarioLogueado != "true")
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }
            base.OnActionExecuting(context);
        }
        #endregion
    }
}

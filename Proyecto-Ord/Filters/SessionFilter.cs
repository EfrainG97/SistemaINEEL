using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Proyecto_Ord.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
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
    }
}

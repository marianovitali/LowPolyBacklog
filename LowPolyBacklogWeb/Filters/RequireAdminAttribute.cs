using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LowPolyBacklogWeb.Filters
{
    public class RequireAdminAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Cookies["AdminMode"] != "true")
            {
                if (request.ContentType != null && request.ContentType.Contains("application/json"))
                {
                    context.Result = new BadRequestObjectResult(new { message = "MODO DEMO: Movimiento denegado." });
                }
                else
                {
                    if (context.Controller is Controller controller)
                    {
                        controller.TempData["ErrorMessage"] = "ACCESS DENIED: No tenés permisos para alterar la base de datos en el entorno demo.";
                    }

                    var referer = request.Headers["Referer"].ToString();
                    if (!string.IsNullOrEmpty(referer))
                    {
                        context.Result = new RedirectResult(referer);
                    }
                    else
                    {
                        context.Result = new RedirectToActionResult("Index", "Library", null);
                    }
                }
            }

            // Si tiene la cookie, la ejecución continúa normalmente
            base.OnActionExecuting(context);
        }
    }
}

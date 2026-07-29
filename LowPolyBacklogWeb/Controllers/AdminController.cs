using Microsoft.AspNetCore.Mvc;

namespace LowPolyBacklogWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Unlock(string pin)
        {
            var realPin = _configuration["AdminPin"];

            // Verificamos que el PIN exista en la config y coincida
            if (!string.IsNullOrEmpty(realPin) && pin == realPin)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30),
                    HttpOnly = true 
                };

                Response.Cookies.Append("AdminMode", "true", options);

                TempData["SuccessMessage"] = "SYSTEM OVERRIDE: Modo Administrador Activado.";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "ACCESS DENIED: PIN incorrecto o no autorizado.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Lock()
        {
            Response.Cookies.Delete("AdminMode");
            TempData["SuccessMessage"] = "SYSTEM LOCKED: Modo solo lectura activado.";
            return RedirectToAction("Index", "Home");
        }
    }
}

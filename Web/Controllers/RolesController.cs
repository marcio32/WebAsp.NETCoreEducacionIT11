using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace CinemaCity.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

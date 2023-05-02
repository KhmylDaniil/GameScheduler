using Microsoft.AspNetCore.Mvc;

namespace GameScheduler.MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

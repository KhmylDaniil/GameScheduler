using GameScheduler.MVC.Localization;
using Microsoft.AspNetCore.Mvc;

namespace GameScheduler.MVC.Controllers
{
    public class LocalizationController : Controller
    {
        [HttpGet]
        public IActionResult Index(string culture)
        {
            if (SupportedCultures.SupportedCulturesIds.Contains(culture))
            {
                Response.Cookies.Delete("Culture");

                CookieOptions cookieOptions = new()
                {
                    MaxAge = TimeSpan.FromDays(14)
                };
                Response.Cookies.Append("Culture", culture, cookieOptions);
            }

            var referer = Request.Headers.Referer;

            return Redirect(referer);
        }
    }
}

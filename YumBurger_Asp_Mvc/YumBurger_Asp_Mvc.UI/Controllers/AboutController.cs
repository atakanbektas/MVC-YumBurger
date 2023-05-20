using Microsoft.AspNetCore.Mvc;

namespace YumBurger_Asp_Mvc.UI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

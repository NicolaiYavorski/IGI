using Microsoft.AspNetCore.Mvc;

namespace WebApp.AspCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Help() => View();
    }
}

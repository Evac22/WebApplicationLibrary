using Microsoft.AspNetCore.Mvc;

namespace WebApplicationLibrary.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

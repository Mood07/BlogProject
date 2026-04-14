using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: /
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace BlogSpider.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

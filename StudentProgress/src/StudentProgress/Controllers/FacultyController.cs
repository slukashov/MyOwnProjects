using Microsoft.AspNetCore.Mvc;

namespace StudentProgress.Controllers
{
    public class FacultyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

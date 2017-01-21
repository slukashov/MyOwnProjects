using Microsoft.AspNetCore.Mvc;


namespace StudentProgress.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}

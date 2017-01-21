using Microsoft.AspNetCore.Mvc;

namespace StudentProgress.Controllers
{
    public class JournalSheetController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}

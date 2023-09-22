using Microsoft.AspNetCore.Mvc;

namespace AdvancedTopic_FinalProject.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Ex3.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public string Test()
        {
            return "It's working";
        }
    }
}

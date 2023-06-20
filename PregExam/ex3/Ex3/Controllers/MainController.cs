using Ex3.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ex3.Controllers
{

    public class MainController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewPosts()
        {
            return View();
        }
        public IActionResult Login(string username)
        {
            HttpContext.Session.SetString("username", username);
            return Redirect("ViewPosts");
        }
        public string Test()
        {
            return "It's working";
        }
    }
}

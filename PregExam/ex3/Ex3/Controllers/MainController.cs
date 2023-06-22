using Ex3.Data;
using Ex3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Org.BouncyCastle.Utilities;
using System.Data.Entity;

namespace Ex3.Controllers
{

    public class MainController : Controller
    {
        private readonly ApplicationDbContext db;

        public MainController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ErrorLogin()
        {
            return View();
        }
        public IActionResult AddPost()
        {
            return View();
        }

		public IActionResult Login(string username, string password)
        {
            var user = db.users.Where(x => x.user==username && x.password==password).ToList();
            if(user.Count() == 0)
            {
                return Redirect("ErrorLogin");
            }
            return Redirect("ViewPosts");
        }
        public string Test()
        {
            return "It's working";
        }
    }
}

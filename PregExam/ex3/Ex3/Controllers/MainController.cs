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
        public IActionResult AddContent()
        {
            return View();
        }
        public IActionResult AddNewContent(List<Content> contents)
        {
            int? userID = HttpContext.Session.GetInt32("userID");
            foreach (Content content in contents)
            {
                content.userID = userID;
                content.date = DateTime.UtcNow;
                db.content.Add(content);
            }
            db.SaveChanges();
            return Redirect("AddContent");
        }

		public IActionResult Login(string username, string password)
        {
            var user = db.users.Where(x => x.user==username && x.password==password).ToList();
            if(user.Count() == 0)
            {
                return Redirect("ErrorLogin");
            }
            HttpContext.Session.SetInt32("userID", user.Last().ID);
            HttpContext.Session.SetString("username", user.Last().user);
            HttpContext.Session.SetInt32("role", user.Last().role);
            return Redirect("AddContent");
        }
        public string Test()
        {
            return "It's working";
        }
    }
}

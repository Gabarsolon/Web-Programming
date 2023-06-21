using Ex3.Data;
using Ex3.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

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
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Index");
            var posts = _context.Post.ToList();
            return View(posts);
        }
        public IActionResult UpdatePost(int id)
		{
			var postToUpdate = _context.Post.Find(id);
			return View(postToUpdate);
		}

		public IActionResult Login(string username)
        {
            HttpContext.Session.SetString("username", username);
            return Redirect("ViewPosts");
        }
        public IActionResult SaveUpdatedPost(int id, int topic_id, string text)
        {
			var post = _context.Post.Find(id);
            post.TopicID = topic_id;
            post.User = HttpContext.Session.GetString("username");
            post.Date = DateTime.UtcNow;
            post.Text = text;
            _context.SaveChanges();
            return Redirect("ViewPosts");
        }
        public string Test()
        {
            return "It's working";
        }
    }
}

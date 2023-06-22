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
            var result = db.Websites
    .Join(
        db.Documents,
        w => w.Id,
        d => d.WebsiteId,
        (w, d) => new { Website = w, Document = d }
    )
    .GroupBy(x => new { x.Website.Id, x.Website.Url })
    .Select(g => new
    {
        Id = g.Key.Id,
        Url = g.Key.Url,
        DocumentCount = g.Count()
    })
    .ToList();
            return View();
        }
        public IActionResult ViewPosts()
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Index");
			List<PostDTO> posts = db.Post.Join(
		        db.Topic,
		        post => post.TopicID,
		        topic => topic.Id,
		        (post, topic) => new PostDTO(post, topic)
	        ).ToList();
			return View(posts);
        }
        public IActionResult AddPost()
        {
            return View();
        }
        public IActionResult UpdatePost(int id)
		{
			var postToUpdate = db.Post.Find(id);
			return View(postToUpdate);
		}

		public IActionResult Login(string username)
        {
            HttpContext.Session.SetString("username", username);
            HttpContext.Session.SetInt32("noOfPosts", db.Post.Count());
            return Redirect("ViewPosts");
        }
        public IActionResult AddNewPost(string topic_name, string text)
        {
            string username = HttpContext.Session.GetString("username");
            DateTime currentDateTime = DateTime.UtcNow;
            var topic = db.Topic.FirstOrDefault(topic => topic.TopicName == topic_name);

            if (topic == null)
            {
                topic = new Topic { TopicName = topic_name };
                db.Topic.Add(topic);
                db.SaveChanges();
            }

            db.Post.Add(new Post{User=username, TopicID = topic.Id, Date=currentDateTime, Text = text });
            db.SaveChanges();
			HttpContext.Session.SetInt32("noOfPosts", db.Post.Count());
			return RedirectToAction("ViewPosts");
        }
        public IActionResult SaveUpdatedPost(int id, int topic_id, string text)
        {
			var post = db.Post.Find(id);
            post.TopicID = topic_id;
            post.User = HttpContext.Session.GetString("username");
            post.Date = DateTime.UtcNow;
            post.Text = text;
            db.SaveChanges();
            return Redirect("ViewPosts");
        }
        public IActionResult CheckNewPost()
        {
            int? previouslyNoOfPosts = HttpContext.Session.GetInt32("noOfPosts");
            int currentNoOfPosts = db.Post.Count();
            if (previouslyNoOfPosts != null && db.Post.Count() != previouslyNoOfPosts)
            {
                HttpContext.Session.SetInt32("noOfPosts", currentNoOfPosts);
				return Json(db.Post.OrderByDescending(post => post.Id).FirstOrDefault());
			}

            return Ok(null);
		}
        public string Test()
        {
            return "It's working";
        }
    }
}

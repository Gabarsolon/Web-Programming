﻿using Ex3.Data;
using Ex3.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using System.Data.Entity;

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
        public IActionResult AddPost()
        {
            return View();
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
        public IActionResult AddNewPost(string topic_name, string text)
        {
            string username = HttpContext.Session.GetString("username");
            DateTime currentDateTime = DateTime.UtcNow;
            var topic = _context.Topic.FirstOrDefault(topic => topic.TopicName == topic_name);

            if (topic == null)
            {
                topic = new Topic { TopicName = topic_name };
                _context.Topic.Add(topic);
                _context.SaveChanges();
            }

            _context.Post.Add(new Post{User=username, TopicID = topic.Id, Date=currentDateTime, Text = text });
            _context.SaveChanges();
            return RedirectToAction("ViewPosts");
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

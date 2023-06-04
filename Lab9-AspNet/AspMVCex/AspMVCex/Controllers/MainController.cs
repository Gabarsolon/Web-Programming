using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AspMVCex.Models;
using AspMVCex.DataAbstractionLayer;

namespace AspMVCex.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View("FilterRecipes");
        }

        public string TestController()
        {
            return "Testing the MainController .. OK!";
        }

        public ActionResult AddStudent()
        {
            return View("AddNewStudent");
        }

        //public ActionResult SaveStudent()
        //{
        //    Recipe stud = new Recipe();
        //    stud.Id = int.Parse(Request.Params["id"]);
        //    stud.Nume = Request.Params["nume"];
        //    stud.Password = Request.Params["password"];
        //    stud.Group_id = int.Parse(Request.Params["group_id"]);

        //    DAL dal = new DAL();
        //    dal.SaveStudent(stud);
        //    return RedirectToAction("GetStudents");
        //}

        public string GetRecipesByType()
        {
            string type = Request.Params["type"];
            DAL dal = new DAL();
            List<Recipe> recipeList = dal.GetRecipesByType(type);
            ViewData["recipeList"] = recipeList;

            string result = "" +
                "<table>" +
                    "<thead>" +
                        "<th>Id</th>" +
                        "<th>Author</th>" +
                        "<th>Name</th>" +
                        "<th>Type</th>" +
                        "<th>Preparation Time</th>" + 
                        "<th>Servings</th>" +
                        "<th>Ingredients</th>" +
                        "<th>Method</th>" +
                    "</thead>";

            foreach (Recipe recipe in recipeList)
            {
                result += 
                    "<tr>" +
                        "<td>" + recipe.Id + "</td>" +
                        "<td>" + recipe.author + "</td>" +
                        "<td>" + recipe.name + "</td>" +
                        "<td>" + recipe.type + "</td>" +
                        "<td>" + recipe.prep_time + "</td>" +
                        "<td>" + recipe.servings + "</td>" +
                        "<td>" + recipe.ingredients + "</td>" +
                        "<td>" + recipe.method + "</td>" +
                        "<td>" +
                    "</tr>";
            }

            result += "</table>";
            return result;
        }
    }
}
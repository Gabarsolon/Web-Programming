using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AspMVCex.Models;
using AspMVCex.DataAbstractionLayer;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace AspMVCex.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult AddNewRecipe()
        {
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                return RedirectToAction("Index");
            return View("AddRecipe");
        }
        public ActionResult UpdateRecipeView()
        {
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                return RedirectToAction("Index");
            return View("UpdateRecipe");
        }
        public ActionResult FilterRecipes()
        {
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                return RedirectToAction("Index");
            return View("FilterRecipes");
        }
        public ActionResult Login()
        {

            string username = Request["username"];
            string password = Request["password"];

            DAL dal = new DAL();
            if (dal.Login(username, password))
            {
                Session["LoggedIn"] = true;
                return View("FilterRecipes");
            }
            return View("ErrorLogin");
        }
        public ActionResult AddRecipe()
        {
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                return RedirectToAction("Index");
            Recipe recipe = new Recipe();
            recipe.author = Request["author"];
            recipe.name = Request["name"];
            recipe.type = Request["type"];
            recipe.prep_time = Request["prep_time"];
            recipe.servings = int.Parse(Request["servings"]);
            recipe.ingredients = Request["ingredients"];
            recipe.method = Request["method"];
           
            DAL dal = new DAL();
            dal.AddRecipe(recipe);

            return RedirectToAction("FilterRecipes");
        }
        public ActionResult RemoveRecipe()
        {
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                return RedirectToAction("Index");
            int id = int.Parse(Request.Params["id"]);

            DAL dal = new DAL();
            dal.RemoveRecipe(id);

            return RedirectToAction("FilterRecipes");
        }
        public ActionResult UpdateRecipe()
        {
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                return RedirectToAction("Index");

            Recipe recipe = new Recipe();
            recipe.id = int.Parse(Request["id"]);
            recipe.author = Request["author"];
            recipe.name = Request["name"];
            recipe.type = Request["type"];
            recipe.prep_time = Request["prep_time"];
            recipe.servings = int.Parse(Request["servings"]);
            recipe.ingredients = Request["ingredients"];
            recipe.method = Request["method"];

            DAL dal = new DAL();
            dal.UpdateRecipe(recipe);

            return RedirectToAction("FilterRecipes");

        }
        public string GetRecipeById()
        {
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                return null;

            int id = int.Parse(Request.Params["id"]);
            DAL dal = new DAL();

            Recipe recipe = dal.GetRecipeById(id);

            return JsonConvert.SerializeObject(recipe);
        }
        public string GetRecipesByType()
        {
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                return null;

            string type = Request.Params["type"];
            DAL dal = new DAL();
            List<Recipe> recipeList = dal.GetRecipesByType(type);

            string result = "" +
                "<table>" +
                    "<thead>" +
                        "<th>Operations</th>" +
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
                        "<td>" +
                            "<a href =\"/Main/UpdateRecipeView?id=" +  recipe.id + "\"><button>Update</button></a><br>" +	
                            "<button id =\"remove-button\">Remove</button>" +
                        "</td>" +
                        "<td>" + recipe.id + "</td>" +
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
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

        public ActionResult AddNewRecipe()
        {
            return View("AddRecipe");
        }

        public void AddRecipe()
        {
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
        }

        public string GetRecipesByType()
        {
            string type = Request.Params["type"];
            DAL dal = new DAL();
            List<Recipe> recipeList = dal.GetRecipesByType(type);

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
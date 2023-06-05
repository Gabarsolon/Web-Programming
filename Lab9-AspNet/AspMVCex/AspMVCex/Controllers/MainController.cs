using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AspMVCex.Models;
using AspMVCex.DataAbstractionLayer;
using System.Web.UI.WebControls;

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
        public void RemoveRecipe()
        {
            int id = int.Parse(Request.Params["id"]);

            DAL dal = new DAL();
            dal.RemoveRecipe(id);
        }

        public string GetRecipesByType()
        {
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
                            "<a href =\"update.html?id=" +  recipe.id + "\"><button>Update</button></a><br>" +	
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
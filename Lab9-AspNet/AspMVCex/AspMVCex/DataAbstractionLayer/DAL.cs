using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AspMVCex.Models;
using MySql.Data.MySqlClient;

namespace AspMVCex.DataAbstractionLayer
{
    public class DAL
    {
        public List<Recipe> GetRecipesByType(string type)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=food_recipes;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from recipes where type LIKE '%" + @type + "%'";
                cmd.Parameters.AddWithValue("type", type);
                MySqlDataReader myreader = cmd.ExecuteReader();

                List<Recipe> recipes = new List<Recipe>();
                while (myreader.Read())
                {
                    Recipe recipe = new Recipe();
                    recipe.Id = myreader.GetInt32("id");
                    recipe.author = myreader.GetString("author");
                    recipe.name = myreader.GetString("name");
                    recipe.type = myreader.GetString("type");
                    recipe.prep_time = myreader.GetString("prep_time");
                    recipe.servings = myreader.GetInt32("servings");
                    recipe.ingredients = myreader.GetString("ingredients");
                    recipe.method = myreader.GetString("method");
                    recipes.Add(recipe);
                }
                myreader.Close();
                return recipes;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return null;

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMVCex.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string author { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string prep_time { get; set; }
        public int servings { get; set; }
        public string ingredients { get; set; }
        public string method { get; set; }

    }
}
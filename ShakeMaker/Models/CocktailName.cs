using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public class CocktailName
    {
        public string Cocktail_Name { get; set; }
        public string category { get; set; }

        public CocktailName(string name, Category cat)
        {
            Cocktail_Name = name;
            category = cat.ToString();
        }

    }
}
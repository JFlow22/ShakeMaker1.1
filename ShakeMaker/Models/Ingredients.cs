using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public class Ingredient
    {

        public string name { get; set; }

        public string amount { get; set; }

        public Ingredient(string ingName, string ingAmount)
        {
            name = ingName;
            amount = ingAmount;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public class Ingredient
    {

        public string name { get; set; }

        public int amount { get; set; }

        public Ingredient(string ingName, int ingAmount)
        {
            name = ingName;
            amount = ingAmount;
        }

    }
}
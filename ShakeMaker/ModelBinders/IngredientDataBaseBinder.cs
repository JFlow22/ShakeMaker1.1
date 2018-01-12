using ShakeMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShakeMaker.ModelBinders
{
    public class IngredientDataBaseBinder
    {
        [Key]
        public int cid { get; set; }
        [Key]
        public string ing { get; set; }
        public string amount { get; set; }

        public Ingredient getIngredient()
        {
            return new Ingredient(ing, amount);
        }
    }
}
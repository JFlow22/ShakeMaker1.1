using ShakeMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShakeMaker.ModelBinders
{
    // Binder class for ingredients table
    public class IngredientDataBaseBinder
    {
        [Key ,Column (Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cid { get; set; }
        [Key, Column (Order = 1)]
        public string ing { get; set; }
        public string amount { get; set; }

        public Ingredient getIngredient()
        {
            return new Ingredient(ing, amount);
        }
    }
}
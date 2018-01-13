using ShakeMaker.Dal;
using ShakeMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShakeMaker.ModelBinders
{
    public class CocktailDataBaseBinder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cid { get; set; }
        public int category { get; set; }
        public string preperation { get; set; }
        public string video { get; set; }
        public string name { get; set; }
        public Cocktails getCocktail()
        {
            IngredientDal cocDal = new IngredientDal();
            List<Ingredient> ings = cocDal.findIngredients(cid);
            Category cat = (Category)category;
            return new Cocktails(cid, ings, cat, preperation, video, name);
        }
    }
}
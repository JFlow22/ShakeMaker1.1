using ShakeMaker.ModelBinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ShakeMaker.Models
{
    /*
     * frozenNblended = Frozen Cocktails and Blended Drinks
     * tikiNtropical = Tiki Cocktail Recipes & Tropical Drinks
    */
    public enum Category { classic, holiday, spring, frozenNblended, hotAlcoholic, tikiNtropical }

    public class Cocktails
    {
        public int cid { get; set; }

        public List<Ingredient> ing { get; set; }

        public Category cocktailCategory { get; set; }

        public string preperation { get; set; }

        public string videoLink { get; set; }

        public string name { get; set; }
        public Cocktails(int id, List<Ingredient> ingredients, Category cat, string prep, string video, string n)
        {
            cid = id;
            ing = new List<Ingredient>(ingredients);
            cocktailCategory = cat;
            preperation = prep;
            videoLink = video;
            name = n;
        }

        public CocktailDataBaseBinder toDB()
        {
            CocktailDataBaseBinder coc = new CocktailDataBaseBinder();
            coc.cid = cid;
            coc.category = (int)cocktailCategory;
            coc.preperation = preperation;
            coc.video = videoLink;
            coc.name = name;
            return coc;
        }

        public List<IngredientDataBaseBinder> ingstoDB()
        {
            List<IngredientDataBaseBinder> ings = new List<IngredientDataBaseBinder>();
            foreach (Ingredient ingredient in ing)
            {
                IngredientDataBaseBinder ingdb = new IngredientDataBaseBinder();
                ingdb.cid = cid;
                ingdb.ing = ingredient.name;
                ingdb.amount = ingredient.amount;
                ings.Add(ingdb);
            }
            return ings;
        }

        public void addIngredient(Ingredient ingredient)
        {
            ing.Add(ingredient);
        }

    }
}
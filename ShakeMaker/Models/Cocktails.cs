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

        public Cocktails(int id, List<Ingredient> ingredients, Category cat, string prep, string video)
        {
            cid = id;
            ing = new List<Ingredient>(ingredients);
            cocktailCategory = cat;
            preperation = prep;
            videoLink = video;
        }

        public void append(string ingr, int amount)
        {
            ing.Add(new Ingredient(ingr, amount));
        }

    }
}
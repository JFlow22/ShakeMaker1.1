using ShakeMaker.ModelBinders;
using ShakeMaker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShakeMaker.Dal
{
    public class IngredientDal : DbContext
    {
        public DbSet<IngredientDataBaseBinder> ingredients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IngredientDataBaseBinder>().ToTable("Ingredients");
        }

        public List<Ingredient> findIngredients(int cid)
        {
            List<Ingredient> ings = new List<Ingredient>();
            foreach (IngredientDataBaseBinder ingredient in ingredients)
            {
                if (ingredient.cid == cid)
                    ings.Add(ingredient.getIngredient());
            }
            return ings;
        }

        public void addIngredient(Ingredient ing, int cid)
        {
            IngredientDataBaseBinder ingdb = new IngredientDataBaseBinder();
            ingdb.ing = ing.name;
            ingdb.amount = ing.amount;
            ingdb.cid = cid;
            ingredients.Add(ingdb);
            SaveChanges();
        }

        public bool locate(string ingredient, int cid)
        {
            foreach(IngredientDataBaseBinder ing in ingredients)
            {
                if (ing.ing == ingredient && ing.cid == cid)
                    return true;
            }
            return false;
        }
    }
}
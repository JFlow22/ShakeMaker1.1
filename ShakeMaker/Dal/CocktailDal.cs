using ShakeMaker.ModelBinders;
using ShakeMaker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShakeMaker.Dal
{
    public class CocktailDal : DbContext
    {
        public DbSet<CocktailDataBaseBinder> cocktails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CocktailDataBaseBinder>().ToTable("Cocktails");
        }

        public void addCocktail(Cocktails coc)
        {
            cocktails.Add(coc.toDB());
            SaveChanges();
        }

        public void addIngredient(Ingredient ing, int cid)
        {
            IngredientDal ingDal = new IngredientDal();
            ingDal.addIngredient(ing, cid);
        }

        public Cocktails findCocktail(int cid)
        {
            foreach(CocktailDataBaseBinder coc in cocktails)
            {
                if (coc.cid == cid)
                    return coc.getCocktail();
            }
            return null;
        }
    }
}
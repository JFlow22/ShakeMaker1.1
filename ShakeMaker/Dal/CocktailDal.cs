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
        public DbSet<Cocktails> cocktails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Admin>().ToTable("Cocktails");
        }

        public void addCocktail(Cocktails coc)
        {
        }

        public void addIngredient(Ingredient ing, int cid)
        {
        }
    }
}
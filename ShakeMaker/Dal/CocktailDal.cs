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
        const string connectionString = "Server=shakerservice.database.windows.net;Initial Catalog=ShakeMaker;Persist Security Info=True;User ID=main;Password=shakemaker1!";
        public DbSet<Cocktails> cocktails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Admin>().ToTable("Cocktails");
        }

        public void addCocktail(Cocktails coc)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "insert into Cocktails values (" + coc.cid + "," + Int32.Parse(coc.cocktailCategory.ToString()) + ",'" + coc.preperation + "','" + coc.videoLink + "')";
            SqlDataReader reader = cmd.ExecuteReader();
            con.Close();
        }

        public void addIngredient(Ingredient ing, int cid)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "insert into Ingredients values (" + cid + ",'" + ing.name + "','" + ing.amount + "')";
            SqlDataReader reader = cmd.ExecuteReader();
            con.Close();
        }
    }
}
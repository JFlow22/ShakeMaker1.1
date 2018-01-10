namespace ShakeMaker.Dal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using ShakeMaker.Models;
    using System.Data.SqlClient;

    public partial class GetCocktails : DbContext
    {
        const string connectionString = "Server=shakerservice.database.windows.net;Initial Catalog=ShakeMaker;Persist Security Info=True;User ID=main;Password=shakemaker1!";
        List<Cocktails> cocktails = new List<Cocktails>();
        public GetCocktails()
            : base("name=GetAdmins")
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Cocktails";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int cid = Int32.Parse(reader["CID"].ToString());
                Category cat = (Category)Int32.Parse(reader["CATEGORY"].ToString());
                string preperation = reader["PREPEREATION"].ToString();
                string video = reader["VIDEO"].ToString();
                List<Ingredient> ings = ingredientsForCocktail(cid);
                cocktails.Add(new Cocktails(cid, ings, cat, preperation, video));
            }
            con.Close();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Admin>().ToTable("Admins");
        }

        public List<Ingredient> ingredientsForCocktail(int cocktail)
        {
            List<Ingredient> ings = new List<Ingredient>();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Ingredients";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int cid = Int32.Parse(reader["CID"].ToString());
                string ing = reader["ING"].ToString();
                string amount = reader["AMOUNT"].ToString();
                if (cid==cocktail)
                {
                    ings.Add(new Ingredient(ing, amount));
                }
            }
            con.Close();
            return ings;
        }

        public Cocktails findCocktail(int cid)
        {
            foreach (Cocktails coc in cocktails)
            {
                if (coc.cid == cid)
                    return coc;
            }
            return null;
        }
    }
}

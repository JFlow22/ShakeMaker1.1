using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ShakeMaker.Models;
using System.Data.SqlClient;

namespace ShakeMaker.Dal
{
    public class UserDal : DbContext
    {
        const string connectionString = "Server=shakerservice.database.windows.net;Initial Catalog=ShakeMaker;Persist Security Info=True;User ID=main;Password=shakemaker1!";
        public DbSet<RegularUser> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RegularUser>().ToTable("Users");
        }

        public void addUser(RegularUser user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "insert into Users values ('" + user.userName + "','" + user.userPassword + "','" + user.email + "','" + user.createCocktailString() + "')";
            SqlDataReader reader = cmd.ExecuteReader();
            con.Close();
        }

        public void updateUser(RegularUser user)
        {
            GetRegularUsers ulist = new GetRegularUsers();
            if (!ulist.locate(user)) return;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "update Users set favouriteCocktails = '" + user.createCocktailString() + "' where userName = '" + user.userName + "';";
            SqlDataReader reader = cmd.ExecuteReader();
            con.Close();
        }

    }
}
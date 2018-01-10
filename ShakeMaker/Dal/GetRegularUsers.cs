namespace ShakeMaker.Dal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ShakeMaker.Models;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public partial class GetRegularUsers : DbContext
    {
        const string connectionString = "Server=shakerservice.database.windows.net;Initial Catalog=ShakeMaker;Persist Security Info=True;User ID=main;Password=shakemaker1!";
        List<RegularUser> users= new List<RegularUser>();
        public GetRegularUsers()
            : base("name=GetAdmins")
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Users";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                RegularUser user = new RegularUser(reader["USERNAME"].ToString(), reader["PASSWORD"].ToString(), reader["EMAIL"].ToString());
                user.favCocktails = cocktailList(reader["FAVOURITECOCKTAILS"].ToString());
                users.Add(user);
            }
            con.Close();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RegularUser>().ToTable("Users");
        }

        public bool locate(RegularUser user)
        {
            foreach (RegularUser us in users)
            {
                if (us.userName == user.userName)
                    return true;
            }
            return false;
        }

        public RegularUser findUser(string userName)
        {
            foreach (RegularUser us in users)
            {
                if (us.userName == userName)
                    return us;
            }
            return null;
        }

        List<Cocktails> cocktailList(string strCocktails)
        {
            List<int> cocktailCids = new List<int>();
            List<Cocktails> cocs = new List<Cocktails>();
            for (int i = 0; i < strCocktails.Split(',').Length; i++)
            {
                cocktailCids.Add(Int32.Parse(strCocktails.Split(',')[i]));
            }
            GetCocktails cocktails = new GetCocktails();
            foreach (int cid in cocktailCids)
            {
                cocs.Add(cocktails.findCocktail(cid));
            }
            return cocs;
        }
    }
}

namespace ShakeMaker.Dal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using ShakeMaker.Models;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.EntityClient;
    using System.Diagnostics;
    using System.Data.SqlClient;

    public partial class GetAdmins : DbContext
    {
        const string connectionString = "Server=shakerservice.database.windows.net;Initial Catalog=ShakeMaker;Persist Security Info=True;User ID=main;Password=shakemaker1!";
        List<Admin> admins = new List<Admin>();
        public GetAdmins()
            : base("name=GetAdmins")
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from Admins";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                admins.Add(new Admin(reader["USERNAME"].ToString(), reader["PASSWORD"].ToString()));
            }
            con.Close();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Admin>().ToTable("Admins");
        }

        public bool locate(Admin user)
        {
            foreach (Admin ad in admins)
            {
                if (ad.userName == user.userName)
                    return true;
            }
            return false;
        }

        public Admin findAdmin(string userName)
        {
            foreach (Admin ad in admins)
            {
                if (ad.userName == userName)
                    return ad;
            }
            return null;
        }
    }
}

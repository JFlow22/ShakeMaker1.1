using ShakeMaker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShakeMaker.Dal
{
    public class AdminDal : DbContext
    {
        public DbSet<Admin> admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Admin>().ToTable("Admins");
        }

        public bool locate(Admin admin)
        {
            foreach (Admin ad in admins)
            {
                if (ad.userName == admin.userName)
                    return true;
            }
            return false;
        }

        public Admin findUser(string userName)
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
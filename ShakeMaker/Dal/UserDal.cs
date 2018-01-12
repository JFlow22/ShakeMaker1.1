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
        public DbSet<RegularUser> users { get; set; }

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

        public void addUser(RegularUser user)
        {

        }

        public void updateUser(RegularUser user)
        {

        }

    }
}
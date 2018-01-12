using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ShakeMaker.Models;
using System.Data.SqlClient;
using ShakeMaker.ModelBinders;

namespace ShakeMaker.Dal
{
    public class UserDal : DbContext
    {
        public DbSet<UserDataBaseBinder> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserDataBaseBinder>().ToTable("Users");
        }

        public bool locate(RegularUser user)
        {
            foreach (UserDataBaseBinder us in users)
            {
                if (us.userName == user.userName)
                    return true;
            }
            return false;
        }

        public RegularUser findUser(string userName)
        {
            foreach (UserDataBaseBinder us in users)
            {
                if (us.userName == userName)
                    return us.getUser();
            }
            return null;
        }

        public void addUser(RegularUser user)
        {
            users.Add(user.toDB());
            SaveChanges();
        }

        public void addFavCocktail(string userName, int cid)
        {
            CocktailDal cocDal = new CocktailDal();
            Cocktails coc = cocDal.findCocktail(cid);
            foreach (UserDataBaseBinder us in users)
            {
                if (us.userName == userName)
                {
                    us.getUser().addFavCocktail(coc);
                    us.favouriteCocktails = us.getUser().createCocktailString();
                }
            }
            SaveChanges();
        }

    }
}
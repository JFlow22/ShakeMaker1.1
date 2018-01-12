using ShakeMaker.Dal;
using ShakeMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShakeMaker.ModelBinders
{
    public class UserDataBaseBinder
    {
        [Key, Column (Order = 0)]
        public string userName { get; set; }
        public string password { get; set; }
        [Key, Column (Order = 1)]
        public string email { get; set; }
        public string favouriteCocktails { get; set; }

        public RegularUser getUser()
        {
            RegularUser user = new RegularUser(userName, password, email);
            List<Cocktails> cocktails = stringToCocktails();
            foreach (Cocktails coc in cocktails)
                user.addFavCocktail(coc);
            return user;
        }

        private List<Cocktails> stringToCocktails()
        {
            CocktailDal cocDal = new CocktailDal();
            List<Cocktails> cocktails = new List<Cocktails>();
            foreach (string str in favouriteCocktails.Split(','))
            {
                cocktails.Add(cocDal.findCocktail(Int32.Parse(str)));
            }
            return cocktails;
        }
    }
}
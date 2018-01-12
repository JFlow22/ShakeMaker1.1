using ShakeMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShakeMaker.ModelBinders
{
    public class UserDataBaseBinder
    {
        [Key]
        string userName;
        string password;
        [Key]
        string email;
        string favouriteCocktails;

        public RegularUser getUser()
        {
            RegularUser user = new RegularUser(userName, password, email);
            List<Cocktails> cocktails = new List<Cocktails>();
        }

        public List<Cocktails> stringToCocktails()
        {
            List<Cocktails> cocktails = new List<Cocktails>();
            foreach (string str in favouriteCocktails.Split(','))
            {
                
            }
        }
    }
}
using ShakeMaker.Dal;
using ShakeMaker.ModelBinders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    [Table("Users")]
    public class RegularUser : SuperUser
    {

        public string email { get; set; }
        public List<Cocktails> favCocktails { get; set; }
        public override string getType()
        {
            return "RegularUser";
        }

        public RegularUser(string name, string pass, string emal="")
        {
            userName = name;
            password = pass;
            email = emal;
            favCocktails = new List<Cocktails>();
        }

        public RegularUser()
        {
            favCocktails = new List<Cocktails>();
        }

        public string createCocktailString()
        {
            string str = "";
            foreach(Cocktails coc in favCocktails)
            {
                str += coc.cid;
                str += ',';
            }
            str = str.Remove(str.Length - 1);
            return str;
        }

        public void addFavCocktail(Cocktails coc)
        {
            favCocktails.Add(coc);
        }

        public UserDataBaseBinder toDB()
        {
            UserDataBaseBinder user = new UserDataBaseBinder();
            user.userName = userName;
            user.password = password;
            user.email = email;
            user.favouriteCocktails = createCocktailString();
            return user;
        }

        public bool hasCocktail(Cocktails coc)
        {
            foreach(Cocktails cocktail in favCocktails)
            {
                if (cocktail.cid == coc.cid)
                    return true;
            }
            return false;
        }
    }
}
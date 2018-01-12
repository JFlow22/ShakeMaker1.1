using ShakeMaker.Dal;
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

        public override string getType()
        {
            return "RegularUser";
        }

        public RegularUser(string name, string pass, string emal="")
        {
            userName = name;
            password = pass;
            email = emal;
        }

        public RegularUser() { }

        public List<Cocktails> favCocktails { get; set; }

        public string createCocktailString()
        {
            string str = "";
            foreach(Cocktails coc in favCocktails)
            {
                str += coc.cid;
                str += ',';
            }
            str.TrimEnd(',');
            return str;
        }

        public void addFavCocktail(int cid)
        {
            GetCocktails cocktails = new GetCocktails();
            Cocktails coc = cocktails.findCocktail(cid);
            favCocktails.Add(coc);
            UserDal users = new UserDal();
            
        }
    }
}
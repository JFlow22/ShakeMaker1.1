using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public class regularUser : superUser
    {
        public override string getType()
        {
            return "regularUser";
        }

        public List<Cocktails> favCocktails = new List<Cocktails>();
    }
}
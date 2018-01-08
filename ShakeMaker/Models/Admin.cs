using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public class Admin : superUser
    {
        public override string getType()
        {
            return "Admin";
        }
    }
}
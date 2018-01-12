using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public class Admin : SuperUser
    {
        public override string getType()
        {
            return "Admin";
        }

        public Admin(string name, string pass)
        {
            userName = name;
            password = pass;
        }

        public Admin() { }

    }
}
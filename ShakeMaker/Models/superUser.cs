using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public abstract class superUser
    {
        public string userName { get; set; }

        public string userPassword { get; set; }

        public abstract string getType();
    }
}
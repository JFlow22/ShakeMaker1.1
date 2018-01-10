using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public abstract class SuperUser
    {
        [Required]
        [StringLength(20,MinimumLength = 2)]
        public string userName { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 4)]
        public string userPassword { get; set; }

        public abstract string getType();
    }
}
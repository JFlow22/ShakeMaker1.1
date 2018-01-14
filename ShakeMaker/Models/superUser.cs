using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShakeMaker.Models
{
    public abstract class SuperUser
    {
        [Key]
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "User name must contain at least 2 characters and maximum of 20 characters")]
        public string userName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must contain at least 4 characters and maximum of 20 characters")]
        public string password { get; set; }

        public abstract string getType();
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISBD_project.Models
{
    public class RegisterModel
    {
        [RegularExpression("^[A-Za-z]+$")]
        public string LoginA { get; set; }

        public string PasswordA { get; set; }

        public string RepeatedPasswordA { get; set; }

        public string NameU { get; set; }
        
        public string SurnameU { get; set; }
        
        public string EmailU { get; set; }

        public int AgeU { get; set; }

        public int GenderU { get; set; }

        public int? IdUa { get; set; } = 3;
    }
}
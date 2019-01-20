using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using ISBD_project.Resources;

namespace ISBD_project.Models
{
    public class RegisterModel
    {
        public string LoginA { get; set; }

        [StringLength(6)]
        public string PasswordA { get; set; }

        [Compare("PasswordA")]
        public string RepeatedPasswordA { get; set; }

        public string NameU { get; set; }
        
        public string SurnameU { get; set; }
        
        public string EmailU { get; set; }

        public DateTime BirthDate { get; set; }

        public int GenderU { get; set; }

        public int? IdUa { get; set; } = 3;
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace ISBD_project.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string LoginA { get; set; }

        [StringLength(64, MinimumLength = 6)]
        [Required]
        public string PasswordA { get; set; }

        [Compare("PasswordA")]
        [Required]
        public string RepeatedPasswordA { get; set; }

        public string NameU { get; set; }
        
        public string SurnameU { get; set; }
        
        [Required]
        public string EmailU { get; set; }

        public DateTime BirthDate { get; set; }

        [Range(0,1)]
        public int GenderU { get; set; }

        public int? IdUa { get; set; } = 3;
    }
}
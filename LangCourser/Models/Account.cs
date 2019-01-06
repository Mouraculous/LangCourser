namespace ISBD_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        public int idA { get; set; }

        [Required]
        [StringLength(255)]
        public string loginA { get; set; }

        [Required]
        [StringLength(255)]
        public string passwordA { get; set; }

        public int? idU { get; set; }

        public virtual Users Users { get; set; }
    }
}

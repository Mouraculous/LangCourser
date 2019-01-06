namespace ISBD_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCourseAffiliation")]
    public partial class UserCourseAffiliation
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idU { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idC { get; set; }

        public int? scoreUCA { get; set; }

        public virtual Course Course { get; set; }

        public virtual Users Users { get; set; }
    }
}

namespace ISBD_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            UserCourseAffiliation = new HashSet<UserCourseAffiliation>();
        }

        [Key]
        public int idC { get; set; }

        [Required]
        [StringLength(255)]
        public string nameC { get; set; }

        public int? lecturerC { get; set; }

        public int? idL { get; set; }

        public virtual Language Language { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCourseAffiliation> UserCourseAffiliation { get; set; }
    }
}

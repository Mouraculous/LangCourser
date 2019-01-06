namespace ISBD_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Account = new HashSet<Account>();
            Course = new HashSet<Course>();
            UserCourseAffiliation = new HashSet<UserCourseAffiliation>();
        }

        [Key]
        public int idU { get; set; }

        [Required]
        [StringLength(255)]
        public string nameU { get; set; }

        [Required]
        [StringLength(255)]
        public string surnameU { get; set; }

        [Required]
        [StringLength(255)]
        public string emailU { get; set; }

        public int ageU { get; set; }

        public int genderU { get; set; }

        public int? idUA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Course { get; set; }

        public virtual UserAffiliation UserAffiliation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCourseAffiliation> UserCourseAffiliation { get; set; }
    }
}

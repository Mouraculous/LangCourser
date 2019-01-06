using System.Data.Entity;

namespace ISBD_project.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<UserAffiliation> UserAffiliation { get; set; }
        public virtual DbSet<UserCourseAffiliation> UserCourseAffiliation { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.loginA)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.passwordA)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.nameC)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.UserCourseAffiliation)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.nameL)
                .IsUnicode(false);

            modelBuilder.Entity<UserAffiliation>()
                .Property(e => e.nameUA)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.nameU)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.surnameU)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.emailU)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Course)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.lecturerC);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.UserCourseAffiliation)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}

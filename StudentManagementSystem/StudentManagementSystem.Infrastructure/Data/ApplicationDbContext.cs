using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<StudentClass> StudentClasses => Set<StudentClass>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentClass>()
                .HasKey(sc => new { sc.StudentId, sc.ClassId });

            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentClasses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentClass>()
               .HasOne(sc => sc.Class)
               .WithMany(c => c.StudentClasses)
               .HasForeignKey(sc => sc.ClassId);

            modelBuilder.Entity<Student>()
               .HasIndex(s => s.PhoneNumber)
               .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.EmailId)
                .IsUnique();
        }
    }
}

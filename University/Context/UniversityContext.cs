using University.Entities;
using Microsoft.EntityFrameworkCore;


namespace University.Context
{
    public class UniversityContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGroup>()
                .HasKey(x => new { x.StudentId, x.GroupId });

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Student)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(sg => sg.StudentId);

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Group)
                .WithMany(g => g.StudentGroups)
                .HasForeignKey(sg => sg.GroupId);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.UniqueIdentifier)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}

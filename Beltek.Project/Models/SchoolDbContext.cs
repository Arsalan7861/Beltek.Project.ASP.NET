using Microsoft.EntityFrameworkCore;

namespace Beltek.Project.Models
{
    public class SchoolDbContext: DbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Class>().HasKey(o=> o.Classid);
            modelBuilder.Entity<Student>().HasKey(o=> o.Studentid);
            modelBuilder.Entity<Student>().Property(o=> o.Name).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Student>().Property(o=> o.Surname).HasColumnType("varchar").HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Student>().HasOne(s => s.Class).WithMany(c => c.Students).HasForeignKey(o=> o.Classid);// Making Classid a foreign key for Student. One to Many relationship.
            modelBuilder.Entity<Class>().Property(o=> o.ClassName).HasColumnType("varchar").HasMaxLength(30).IsRequired();
        }
    }
}

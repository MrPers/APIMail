using Mail.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Mail.DB
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<LetterUser> LetterUsers { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<LetterUser>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.LetterUsers)
                .HasForeignKey(sc => sc.UserId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LetterUser>()
                .HasOne(sc => sc.Letter)
                .WithMany(c => c.LetterUsers)
                .HasForeignKey(sc => sc.LetterId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LetterUser>()
                .Property(b => b.Status)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .Property(b => b.Surname)
                .HasDefaultValue("RT");

            base.OnModelCreating(modelBuilder);
        }
    }
}


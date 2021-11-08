using Mail.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.DB
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dispatch> Dispatchs { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dispatch>()
                .Property(b => b.Status)
                .HasDefaultValue(false);

            modelBuilder.Entity<UserGroup>()
                .HasKey(t => new { t.UserId, t.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(pt => pt.Group)
                .WithMany(p => p.UserGroups)
                .HasForeignKey(pt => pt.GroupId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.UserGroups)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserGroup>()
                .Property(pt => pt.GroupId)
                .HasDefaultValue(1);

            base.OnModelCreating(modelBuilder);
        }
    }
}


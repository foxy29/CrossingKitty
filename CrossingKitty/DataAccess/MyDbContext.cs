
using CrossingKitty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingKitty.DataAccess
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity => {
                entity.ToTable("User", "dbo").HasKey(i => i.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.PasswordHash).HasColumnName("Password");
                entity.Property(e => e.Score).HasColumnName("Score");

            });
        }
    }
}

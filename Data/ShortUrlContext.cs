using System;
using Microsoft.EntityFrameworkCore;

namespace ShortURL.Models
{
    public class ShortUrlContext : DbContext
    {
        public ShortUrlContext(DbContextOptions<ShortUrlContext> options)
            : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(@"Server=.;Database=ShortUrlDB;Trusted_Connection=True;");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>()
                    .Property(u => u.LongUrl)
                    .HasColumnName("LongUrl")
                    .HasMaxLength(300);

            modelBuilder.Entity<Url>()
                    .Property(u => u.ShortKey)
                    .HasColumnName("ShortKey")
                    .HasMaxLength(6);
        }
    }
}
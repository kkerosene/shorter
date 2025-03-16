using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using shorter.client.Data.Models;

namespace shorter.client.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity tables are configured

            // Configure the Url entity
            modelBuilder.Entity<Url>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.ShortUrl)
                      .IsUnique();

                // Relationship with ApplicationUser
                entity.HasOne(u => u.User)
                      .WithMany(u => u.Urls)
                      .HasForeignKey(u => u.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Delete URLs when user is deleted

                entity.Property(u => u.LongUrl)
                      .IsRequired()
                      .HasMaxLength(2048); // Max URL length

                entity.Property(u => u.ShortUrl)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(u => u.TotalClicks)
                      .HasDefaultValue(0);

                entity.Property(u => u.DateCreated)
                      .HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
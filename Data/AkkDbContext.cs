using AkkWebService.Models;
using Microsoft.EntityFrameworkCore;

namespace AkkWebService.Data;

public class AkkDbContext : DbContext
{
    public AkkDbContext(DbContextOptions<AkkDbContext> options) : base(options)
    {
    }

    public DbSet<CookieCutter> CookieCutters => Set<CookieCutter>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CookieCutter>(entity =>
        {
            entity.HasKey(e => e.CookieCutterId);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAddOrUpdate();

            entity.HasIndex(e => e.Name);
        });
    }
}

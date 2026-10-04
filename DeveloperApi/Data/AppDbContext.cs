using DeveloperApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeveloperApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Developer> Developers => Set<Developer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(d => d.LastName).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Email).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Specialization).IsRequired().HasMaxLength(50);
            entity.Property(d => d.Salary).HasColumnType("decimal(18,2)");
            entity.HasIndex(d => d.Email).IsUnique();
        });
    }
}

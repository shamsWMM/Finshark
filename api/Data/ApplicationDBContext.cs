using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Portfolio> Portfolio { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Portfolio>().HasKey(p => new { p.UserId, p.StockId });
        modelBuilder.Entity<Portfolio>().HasOne(p => p.User).WithMany(u => u.Portfolios).HasForeignKey(p => p.UserId);
        modelBuilder.Entity<Portfolio>().HasOne(p => p.Stock).WithMany(s => s.Portfolios).HasForeignKey(p => p.StockId);
        var roles = new List<IdentityRole>
        {
            new() { Name = "Admin", NormalizedName = "ADMIN" },
            new() { Name = "User", NormalizedName = "USER" }
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}
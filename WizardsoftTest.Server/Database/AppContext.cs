using Microsoft.EntityFrameworkCore;
using WizardsoftTest.Server.Models;

namespace WizardsoftTest.Server.Database;

public sealed class AppContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public AppContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database/app.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasMany(x => x.Subcategories);
        });
    }
}
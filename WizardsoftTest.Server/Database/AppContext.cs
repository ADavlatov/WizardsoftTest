using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizardsoftTest.Server.Models;

namespace WizardsoftTest.Server.Database;

public sealed class AppContext : DbContext
{
    public DbSet<Category> Categories { get; init; }

    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }

    private class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Subcategories);
        }
    }
}
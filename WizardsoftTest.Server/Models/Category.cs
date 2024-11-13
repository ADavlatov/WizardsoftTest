namespace WizardsoftTest.Server.Models;

public record Category
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public List<Category> Subcategories { get; init; } = new();
    public Guid ParentCategoryId { get; init; }
}
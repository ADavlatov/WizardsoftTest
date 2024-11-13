namespace WizardsoftTest.Client.Entities;

public class Category
{
    public string? Id { get; }
    public string Name { get; set; }
    public List<Category>? Subcategories { get; set; }
    public string? ParentCategoryId { get; set; }
}
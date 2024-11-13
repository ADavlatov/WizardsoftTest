namespace WizardsoftTest.Server.Entities;

public class Category(string name, Guid? parentCategoryId)
{
    public Guid Id { get; }
    public string Name { get; set; } = name;
    public ICollection<Category> Subcategories { get; init; } = new List<Category>();
    public Guid? ParentCategoryId { get; set; } = parentCategoryId;
}
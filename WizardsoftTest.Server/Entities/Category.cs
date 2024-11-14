namespace WizardsoftTest.Server.Entities;

public class Category(string name)
{
    public Guid Id { get; }
    public string Name { get; set; } = name;
    public ICollection<Category> Subcategories { get; init; } = new List<Category>();
}
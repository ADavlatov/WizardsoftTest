namespace WizardsoftTest.Server.Models.Requests;

public class AddCategoryRequest
{
    public string Name { get; set; }
    public string? ParentCategoryId { get; set; }
}
namespace WizardsoftTest.Client.Models;

public record AddCategoryRequest(string Name, Guid? ParentCategoryId);
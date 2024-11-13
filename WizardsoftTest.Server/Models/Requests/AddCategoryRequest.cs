namespace WizardsoftTest.Server.Models.Requests;

public record AddCategoryRequest(string Name, Guid? ParentCategoryId);
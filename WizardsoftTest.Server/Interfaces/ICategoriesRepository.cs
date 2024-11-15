using WizardsoftTest.Server.Entities;

namespace WizardsoftTest.Server.Interfaces;

public interface ICategoriesRepository
{
    Task<List<Category>> GetAllCategories();
    Task<Category> GetCategory(Guid id);
    Task<Category> AddCategory(Category category, Category? parentCategory);
    Task<Category> UpdateCategory(Category category);
    Task<Category> DeleteCategory(Guid id);
}
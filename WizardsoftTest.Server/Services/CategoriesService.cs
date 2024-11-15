using Microsoft.EntityFrameworkCore;
using WizardsoftTest.Server.Entities;
using WizardsoftTest.Server.Interfaces;
using WizardsoftTest.Server.Models;
using WizardsoftTest.Server.Repositories;
using AppContext = WizardsoftTest.Server.Database.AppContext;

namespace WizardsoftTest.Server.Services;

public class CategoriesService
{
    private readonly ICategoriesRepository _repository;

    public CategoriesService(ICategoriesRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _repository.GetAllCategories();
    }

    public async Task<Category> GetCategory(Guid id)
    {
        return await _repository.GetCategory(id);
    }

    public async Task<Category> AddCategory(AddCategoryRequest request)
    {
        if (Guid.TryParse(request.ParentCategoryId, out _))
        {
            var parentCategory = await _repository.GetCategory(Guid.Parse(request.ParentCategoryId));
            return await _repository.AddCategory(new Category(request.Name), parentCategory);
        }
        
        return await _repository.AddCategory(new Category(request.Name), null);
    }

    public async Task<Category> UpdateCategory(UpdateCategoryRequest request)
    {
        var category = await _repository.GetCategory(request.Id);
        category.Name = request.Name;
        
        return await _repository.UpdateCategory(category);
    }

    public async Task<Category> DeleteCategory(Guid id)
    {
        return await _repository.DeleteCategory(id);
    }
}
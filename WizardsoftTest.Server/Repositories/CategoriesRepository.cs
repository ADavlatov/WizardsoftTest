using Microsoft.EntityFrameworkCore;
using WizardsoftTest.Server.Entities;
using WizardsoftTest.Server.Interfaces;

namespace WizardsoftTest.Server.Repositories;

using AppContext = WizardsoftTest.Server.Database.AppContext;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly AppContext _context;

    public CategoriesRepository(AppContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategory(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category> AddCategory(Category category, Category? parentCategory)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category> UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category> DeleteCategory(Guid id)
    {
        var category = await GetCategory(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        return category;
    }
}
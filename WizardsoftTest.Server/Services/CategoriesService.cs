using Microsoft.EntityFrameworkCore;
using WizardsoftTest.Server.Entities;
using WizardsoftTest.Server.Models;
using AppContext = WizardsoftTest.Server.Database.AppContext;

namespace WizardsoftTest.Server.Services;

public class CategoriesService(AppContext db)
{
    public async Task<Category?> AddCategory(AddCategoryRequest request)
    {
        var category = new Category(request.Name);

        if (!Guid.TryParse(request.ParentCategoryId, out _))
        {
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();

            return category;
        }

        var parentCategory = await db.Categories.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.ParentCategoryId));
        if (parentCategory == null)
        {
            return null;
        }

        parentCategory.Subcategories.Add(category);
        await db.SaveChangesAsync();

        return category;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        var categories = await db.Categories.ToListAsync();

        return categories;
    }

    public async Task<Category?> GetCategory(Guid id)
    {
        var category = await db.Categories
            .Include(x => x.Subcategories).FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return null;
        }

        return category;
    }

    public async Task<Category?> UpdateCategory(UpdateCategoryRequest request)
    {
        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (category == null)
        {
            return null;
        }

        category.Name = request.Name;
        db.Categories.Update(category);
        await db.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> DeleteCategory(Guid id)
    {
        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return null;
        }

        db.Categories.Remove(category);
        await db.SaveChangesAsync();

        return category;
    }
}
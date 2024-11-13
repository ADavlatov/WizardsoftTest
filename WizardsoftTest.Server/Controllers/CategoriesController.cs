using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WizardsoftTest.Server.Models;
using WizardsoftTest.Server.Models.Requests;
using AppContext = WizardsoftTest.Server.Database.AppContext;

namespace WizardsoftTest.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController(AppContext db) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(string id)
    {
        var category = await db.Categories.Include(x
            => x.Subcategories).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await db.Categories.ToListAsync();

        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
    {
        Category category;

        if (!Guid.TryParse(request.ParentCategoryId, out _))
        {
            category = new Category
                { Name = request.Name, ParentCategoryId = Guid.Empty };
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();

            return Ok(category);
        }

        var parentCategory =
            await db.Categories.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.ParentCategoryId));
        if (parentCategory == null)
        {
            return NotFound();
        }

        category = new Category
            { Name = request.Name, ParentCategoryId = parentCategory.Id };
        parentCategory.Subcategories.Add(category);
        await db.SaveChangesAsync();

        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request, string id)
    {
        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        if (category == null)
        {
            return NotFound();
        }

        category.Name = request.Name;
        db.Categories.Update(category);
        await db.SaveChangesAsync();

        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        if (category == null)
        {
            return NotFound();
        }

        db.Categories.Remove(category);
        await db.SaveChangesAsync();

        return Ok();
    }
}
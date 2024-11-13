using Microsoft.AspNetCore.Mvc;
using WizardsoftTest.Server.Models;
using WizardsoftTest.Server.Services;
using AppContext = WizardsoftTest.Server.Database.AppContext;

namespace WizardsoftTest.Server.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoriesController(AppContext db) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
    {
        var category = await new CategoriesService(db).AddCategory(request);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var category = await new CategoriesService(db).GetAllCategories();

        return Ok(category);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var category = await new CategoriesService(db).GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
    {
        var category = await new CategoriesService(db).UpdateCategory(request);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var category = await new CategoriesService(db).DeleteCategory(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
}
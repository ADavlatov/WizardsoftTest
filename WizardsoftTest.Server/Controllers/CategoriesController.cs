using Microsoft.AspNetCore.Mvc;
using WizardsoftTest.Server.Interfaces;
using WizardsoftTest.Server.Models;
using WizardsoftTest.Server.Services;
using AppContext = WizardsoftTest.Server.Database.AppContext;

namespace WizardsoftTest.Server.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoriesController(ICategoriesRepository db) : ControllerBase
{
    /// <summary>
    /// Adds a new category to the database.
    /// </summary>
    /// <returns>New category.</returns>
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
    {
        // Add the category using the service
        var category = await new CategoriesService(db).AddCategory(request);

        // Check if the category was successfully added
        if (category == null)
        {
            // Return a NotFound result if the category could not be added
            return NotFound();
        }

        // Return an Ok result with the added category
        return Ok(category);
    }

    /// <summary>
    /// Retrieves a list of all categories from the database.
    /// </summary>
    /// <returns>A list of categories.</returns>
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        // Use the CategoriesService to retrieve a list of all categories
        var category = await new CategoriesService(db).GetAllCategories();

        // Return the list of categories with a 200 OK status code
        return Ok(category);
    }

    /// <summary>
    /// Retrieves a specific category from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>The category with the specified ID, or a NotFound result if the category does not exist.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        // Use the CategoriesService to retrieve a specific category by its ID
        var category = await new CategoriesService(db).GetCategory(id);

        // Return a NotFound result if the category could not be found
        if (category == null)
        {
            return NotFound();
        }

        // Return the category with a 200 OK status code
        return Ok(category);
    }

    /// <summary>
    /// Updates an existing category in the database.
    /// </summary>
    /// <param name="request">The request containing the updated category information.</param>
    /// <returns>The updated category, or a NotFound result if the category does not exist.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
    {
        // Use the CategoriesService to update the category
        var category = await new CategoriesService(db).UpdateCategory(request);

        // Return a NotFound result if the category could not be found
        if (category == null)
        {
            return NotFound();
        }

        // Return the updated category with a 200 OK status code
        return Ok(category);
    }

    /// <summary>
    /// Deletes a category from the database.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>The deleted category, or a NotFound result if the category does not exist.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        // Use the CategoriesService to delete the category
        var category = await new CategoriesService(db).DeleteCategory(id);

        // Return a NotFound result if the category could not be found
        if (category == null)
        {
            return NotFound();
        }

        // Return the deleted category with a 200 OK status code
        return Ok(category);
    }
}
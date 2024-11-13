using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WizardsoftTest.Client.Entities;
using WizardsoftTest.Client.Models;

namespace WizardsoftTest.Client.Pages;

public class IndexModel(HttpClient httpClient) : PageModel
{
    public List<Category> Categories { get; set; }
    public Category SelectedCategory { get; set; }

    public async Task OnGetAsync()
    {
        var response = await httpClient.GetAsync("https://localhost:7186/api/v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        Categories = JsonSerializer.Deserialize<List<Category>>(content);
    }

    public async Task<IActionResult> OnPostAddCategoryAsync(string name, Guid parentId)
    {
        var request = new AddCategoryRequest(name, parentId);
        var response = await httpClient.PostAsJsonAsync("https://localhost:7186/api/v1/categories", request);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }

        return BadRequest();
    }

    public async Task<IActionResult> OnPostUpdateCategoryAsync(Guid id, string name)
    {
        var request = new UpdateCategoryRequest(id, name);
        var response = await httpClient.PutAsJsonAsync("https://localhost:7186/api/v1/categories", request);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }

        return BadRequest();
    }

    public async Task<IActionResult> OnPostDeleteCategoryAsync(Guid id)
    {
        var response = await httpClient.DeleteAsync($"https://localhost:7186/api/v1/categories/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }

        return BadRequest();
    }
}
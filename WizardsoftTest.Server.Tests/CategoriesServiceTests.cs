using Moq;
using WizardsoftTest.Server.Entities;
using WizardsoftTest.Server.Interfaces;
using WizardsoftTest.Server.Models;
using WizardsoftTest.Server.Services;

namespace WizardsoftTest.Server.Tests;

public class CategoriesServiceTests
{
    private Mock<ICategoriesRepository> _repositoryMock;
    private CategoriesService _service;

    public CategoriesServiceTests()
    {
        _repositoryMock = new Mock<ICategoriesRepository>();
        _service = new CategoriesService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllCategories_ReturnsCategories()
    {
        // Arrange
        var categories = new List<Category>
            { new("Category 1"), new("Category 2")};
        _repositoryMock.Setup(r => r.GetAllCategories()).ReturnsAsync(categories);

        // Act
        var result = await _service.GetAllCategories();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Category 1", result[0].Name);
        Assert.Equal("Category 2", result[1].Name);
    }

    [Fact]
    public async Task GetCategory_ReturnsCategory()
    {
        // Arrange
        var category = new Category("Category 1");
        _repositoryMock.Setup(r => r.GetCategory(It.IsAny<Guid>())).ReturnsAsync(category);

        // Act
        var result = await _service.GetCategory(Guid.NewGuid());

        // Assert
        Assert.Equal("Category 1", result.Name);
    }

    [Fact]
    public async Task AddCategory_ReturnsCategory()
    {
        // Arrange
        var category = new Category("Category 1");
        _repositoryMock.Setup(r => r.AddCategory(It.IsAny<Category>(), null)).ReturnsAsync(category);

        // Act
        var result = await _service.AddCategory(new AddCategoryRequest("Category 1", ""));

        // Assert
        Assert.Equal("Category 1", result.Name);
    }

    [Fact]
    public async Task UpdateCategory_ReturnsCategory()
    {
        // Arrange
        var category = new Category("Category 1");
        var id = Guid.NewGuid();
        
        category.Id = id;
        _repositoryMock.Setup(r => r.UpdateCategory(It.IsAny<Category>())).ReturnsAsync(category);

        // Act
        var result = await _service.UpdateCategory(new UpdateCategoryRequest(category.Id, "Category 2"));

        // Assert
        Assert.Equal("Category 2", result.Name);
    }

    [Fact]
    public async Task DeleteCategory_ReturnsCategory()
    {
        // Arrange
        var category = new Category("Category 1");
        _repositoryMock.Setup(r => r.DeleteCategory(It.IsAny<Guid>())).ReturnsAsync(category);

        // Act
        var result = await _service.DeleteCategory(Guid.NewGuid());

        // Assert
        Assert.Equal("Category 1", result.Name);
    }
}
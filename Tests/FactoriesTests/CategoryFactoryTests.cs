using Xunit;
using FinanceAccounting.Factories;
using FinanceAccounting.Models;
using FinanceAccounting.DTO;

namespace Tests.FactoriesTests;

public class CategoryFactoryTests
{
    private readonly CategoryFactory _factory = new();

    [Theory]
    [InlineData(CategoryType.Income)]
    [InlineData(CategoryType.Expense)]
    public void CreateCategory_WithParameters_ReturnsValidObject(CategoryType type)
    {
        var category = _factory.CreateCategory(1, "Test Category", type);
        
        Assert.Equal(1, category.Id);
        Assert.Equal("Test Category", category.Name);
        Assert.Equal(type, category.Type);
    }

    [Fact]
    public void CreateCategory_FromDto_ReturnsValidObject()
    {
        var dto = new CategoryDto 
        { 
            Id = 3, 
            Name = "DTO Category", 
            Type = CategoryType.Expense 
        };
        
        var category = _factory.CreateCategory(dto);
        
        Assert.Equal(dto.Id, category.Id);
        Assert.Equal(dto.Name, category.Name);
        Assert.Equal(dto.Type, category.Type);
    }

    [Fact]
    public void CreateCategory_WithInvalidName_ThrowsException()
    {
        var dto = new CategoryDto { Name = "   " };
        
        Assert.Throws<ArgumentException>(() => _factory.CreateCategory(dto));
    }
}
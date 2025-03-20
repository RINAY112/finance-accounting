using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.Facades;
using FinanceAccounting.Models;
using FinanceAccounting.Services.Interfaces;
using Moq;
using Xunit;

namespace Tests.FacadesTests;

public class CategoryFacadeTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock = new();
    private readonly Mock<IDataTransferManager> _dataTransferManagerMock = new();
    private readonly CategoryFacade _facade;

    public CategoryFacadeTests()
    {
        _facade = new CategoryFacade(
            _categoryServiceMock.Object,
            _dataTransferManagerMock.Object
        );
    }

    [Fact]
    public void GetCategoriesByType_ReturnsFilteredCategories()
    {
        var categories = new List<Category>
        {
            new(1, "Food", CategoryType.Expense),
            new(2, "Salary", CategoryType.Income)
        };

        _categoryServiceMock.Setup(m => m.GetCategoriesByType(It.IsAny<CategoryType>()))
            .Returns(categories.Where(c => c.Type == CategoryType.Expense).ToList());
        
        var result = _facade.GetCategoriesByType(CategoryType.Expense);
        
        Assert.Single(result);
        Assert.All(result, c => Assert.Equal(CategoryType.Expense, c.Type));
    }
}
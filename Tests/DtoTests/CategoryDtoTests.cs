using Xunit;
using FinanceAccounting.DTO;
using FinanceAccounting.Models;

namespace Tests.DtoTests;

public class CategoryDtoTests
{
    [Fact]
    public void CategoryDto_Properties_SetCorrectly()
    {
        var dto = new CategoryDto
        {
            Id = 5,
            Name = "Utilities",
            Type = CategoryType.Expense
        };
        
        Assert.Equal(5, dto.Id);
        Assert.Equal("Utilities", dto.Name);
        Assert.Equal(CategoryType.Expense, dto.Type);
    }

    [Fact]
    public void CategoryDto_DefaultValues_AreCorrect()
    {
        var dto = new CategoryDto();
        
        Assert.Equal(0, dto.Id);
        Assert.Null(dto.Name);
        Assert.Equal(default(CategoryType), dto.Type);
    }
}
using Xunit;
using FinanceAccounting.Models;

namespace Tests.ModelsTests;

public class CategoryTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesCategory()
    {
        var category = new Category(1, "Food", CategoryType.Expense);
        
        Assert.Equal(1, category.Id);
        Assert.Equal("Food", category.Name);
        Assert.Equal(CategoryType.Expense, category.Type);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Name_SetInvalidValue_ThrowsArgumentException(string invalidName)
    {
        var category = new Category(1, "Valid", CategoryType.Income);
        
        Assert.Throws<ArgumentException>(() => category.Name = invalidName);
    }
}
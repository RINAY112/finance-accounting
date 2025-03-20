using FinanceAccounting.Data.Converters;
using FinanceAccounting.Models;
using Xunit;

namespace Tests.DataTests.ConvertersAndMapsTests;

public class CsvEnumConverterTests
{
    [Fact]
    public void ConvertToString_EnumValue_ReturnsString()
    {
        var converter = new CsvEnumConverter<CategoryType>();
        var result = converter.ConvertToString(CategoryType.Expense, null, null);
        Assert.Equal("Expense", result);
    }

    [Fact]
    public void ConvertFromString_ValidString_ReturnsEnum()
    {
        var converter = new CsvEnumConverter<CategoryType>();
        var result = converter.ConvertFromString("Income", null, null);
        Assert.Equal(CategoryType.Income, result);
    }

    [Fact]
    public void ConvertFromString_InvalidValue_ThrowsException()
    {
        var converter = new CsvEnumConverter<CategoryType>();
        Assert.Throws<ArgumentException>(() => 
            converter.ConvertFromString("Invalid", null, null));
    }
}
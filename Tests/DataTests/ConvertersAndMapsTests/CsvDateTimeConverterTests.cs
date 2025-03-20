using CsvHelper.Configuration;
using FinanceAccounting.Data.Converters;
using FinanceAccounting.Models;
using Xunit;

namespace Tests.DataTests.ConvertersAndMapsTests;

public class CsvDateTimeConverterTests
{
    [Fact]
    public void CsvDateTimeConverter_RoundTrip_WorksCorrectly()
    {
        var converter = new CsvDateTimeConverter();
        var testDate = new DateTime(2023, 10, 5, 14, 30, 0);
        
        var str = converter.ConvertToString(testDate, null, new MemberMapData(null));
        var result = converter.ConvertFromString(str, null, null);
        
        Assert.Equal("2023-10-05 14:30:00", str);
        Assert.Equal(testDate, result);
    }

    [Fact]
    public void CsvEnumConverter_SerializesCorrectly()
    {
        var converter = new CsvEnumConverter<CategoryType>();
        
        Assert.Equal("Income", converter.ConvertToString(CategoryType.Income, null, null));
        Assert.Equal(CategoryType.Expense, converter.ConvertFromString("Expense", null, null));
    }
}
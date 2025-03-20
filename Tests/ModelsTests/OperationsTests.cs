using Xunit;
using FinanceAccounting.Models;

namespace Tests.ModelsTests;

public class OperationTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesOperation()
    {
        var date = DateTime.Now;
        
        var operation = new Operation(1, 100m, date, 1, 1, "Test");
        
        Assert.Equal(1, operation.Id);
        Assert.Equal(100m, operation.Amount);
        Assert.Equal(date, operation.Date);
        Assert.Equal(1, operation.BankAccountId);
        Assert.Equal(1, operation.CategoryId);
        Assert.Equal("Test", operation.Description);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Constructor_InvalidAmount_ThrowsArgumentException(decimal amount)
    {
        Assert.Throws<ArgumentException>(() => 
            new Operation(1, amount, DateTime.Now, 1, 1));
    }

    [Fact]
    public void Description_SetNull_ConvertsToEmptyString()
    {
        var operation = new Operation(1, 100m, DateTime.Now, 1, 1);
        
        operation.Description = null;
        
        Assert.Equal(string.Empty, operation.Description);
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        var date = new DateTime(2023, 1, 1);
        var operation = new Operation(1, 100m, date, 123, 456, "Test");
        
        var result = operation.ToString();
        
        Assert.Equal("ID: 1 | Amount: 100 | Creation date: 01.01.2023 0:00:00 | Account ID: 123 | Category ID: 456\nDescription:\n\"Test\"", result);
    }
}
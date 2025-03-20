using Xunit;
using FinanceAccounting.DTO;

namespace Tests.DtoTests;

public class OperationDtoTests
{
    [Fact]
    public void OperationDto_Properties_SetCorrectly()
    {
        var testDate = DateTime.Now;
        
        var dto = new OperationDto
        {
            Id = 10,
            Amount = 200.50m,
            Date = testDate,
            Description = "Test description",
            BankAccountId = 3,
            CategoryId = 2
        };
        
        Assert.Equal(10, dto.Id);
        Assert.Equal(200.50m, dto.Amount);
        Assert.Equal(testDate, dto.Date);
        Assert.Equal("Test description", dto.Description);
        Assert.Equal(3, dto.BankAccountId);
        Assert.Equal(2, dto.CategoryId);
    }

    [Fact]
    public void OperationDto_DefaultValues_AreCorrect()
    {
        var dto = new OperationDto();
        
        Assert.Equal(0, dto.Id);
        Assert.Equal(0m, dto.Amount);
        Assert.Equal(default(DateTime), dto.Date);
        Assert.Null(dto.Description);
        Assert.Equal(0, dto.BankAccountId);
        Assert.Equal(0, dto.CategoryId);
    }

    [Fact]
    public void OperationDto_Description_AcceptsNull()
    {
        var dto = new OperationDto
        {
            Description = null
        };
        
        Assert.Null(dto.Description);
    }
}
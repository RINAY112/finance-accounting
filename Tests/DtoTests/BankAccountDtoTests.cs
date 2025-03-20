using Xunit;
using FinanceAccounting.DTO;

namespace Tests.DtoTests;

public class BankAccountDtoTests
{
    [Fact]
    public void BankAccountDto_Properties_SetCorrectly()
    {
        var dto = new BankAccountDto
        {
            Id = 1,
            Name = "Test Account",
            Balance = 1500.75m
        };
        
        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Account", dto.Name);
        Assert.Equal(1500.75m, dto.Balance);
    }

    [Fact]
    public void BankAccountDto_DefaultValues_AreCorrect()
    {
        var dto = new BankAccountDto();
        
        Assert.Equal(0, dto.Id);
        Assert.Null(dto.Name);
        Assert.Equal(0m, dto.Balance);
    }
}
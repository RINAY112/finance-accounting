using Xunit;
using FinanceAccounting.Models;
using FinanceAccounting.Factories;
using FinanceAccounting.DTO;

namespace Tests.FactoriesTests;

public class BankAccountFactoryTests
{
    private readonly BankAccountFactory _factory = new();

    [Fact]
    public void CreateAccount_WithParameters_ReturnsValidObject()
    {
        var account = _factory.CreateAccount(1, "Test Account", 1500m);
        
        Assert.Equal(1, account.Id);
        Assert.Equal("Test Account", account.Name);
        Assert.Equal(1500m, account.Balance);
    }

    [Fact]
    public void CreateAccount_FromDto_ReturnsValidObject()
    {
        var dto = new BankAccountDto 
        { 
            Id = 2, 
            Name = "DTO Account", 
            Balance = 2000m 
        };
        
        var account = _factory.CreateAccount(dto);
        
        Assert.Equal(dto.Id, account.Id);
        Assert.Equal(dto.Name, account.Name);
        Assert.Equal(dto.Balance, account.Balance);
    }

    [Fact]
    public void CreateAccount_FromDtoWithNegativeBalance_ThrowsException()
    {
        var dto = new BankAccountDto { Balance = -100 };
        
        Assert.Throws<ArgumentException>(() => _factory.CreateAccount(dto));
    }
}
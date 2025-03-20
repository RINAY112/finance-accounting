using Xunit;
using FinanceAccounting.Models;

namespace Tests.ModelsTests;

public class BankAccountTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesAccount()
    {
        var account = new BankAccount(1, "Test Account", 100m);
        
        Assert.Equal(1, account.Id);
        Assert.Equal("Test Account", account.Name);
        Assert.Equal(100m, account.Balance);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Name_SetInvalidValue_ThrowsArgumentException(string invalidName)
    {
        var account = new BankAccount(1, "Valid Name");

        Assert.Throws<ArgumentException>(() => account.Name = invalidName);
    }

    [Fact]
    public void Deposit_ValidAmount_UpdatesBalance()
    {
        var account = new BankAccount(1, "Test", 100m);
        
        account.Deposit(50m);
        
        Assert.Equal(150m, account.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Deposit_InvalidAmount_ThrowsArgumentException(decimal amount)
    {
        var account = new BankAccount(1, "Test");
        
        Assert.Throws<ArgumentException>(() => account.Deposit(amount));
    }

    [Fact]
    public void Withdraw_ValidAmount_UpdatesBalance()
    {
        var account = new BankAccount(1, "Test", 100m);
        
        account.Withdraw(40m);
        
        Assert.Equal(60m, account.Balance);
    }

    [Fact]
    public void Withdraw_AmountExceedsBalance_ThrowsInvalidOperationException()
    {
        var account = new BankAccount(1, "Test", 50m);
        
        Assert.Throws<InvalidOperationException>(() => account.Withdraw(100m));
    }
}
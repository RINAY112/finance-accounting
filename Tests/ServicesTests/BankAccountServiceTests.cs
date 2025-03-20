using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.Services;
using Moq;
using Xunit;

namespace Tests.ServicesTests;

public class BankAccountServiceTests
{
    private readonly Mock<IBankAccountFactory> _factoryMock = new();
    private readonly BankAccountService _service;

    public BankAccountServiceTests()
    {
        _service = new BankAccountService(_factoryMock.Object);
    }

    [Fact]
    public void CreateAccount_WithUniqueId_AddsToCollection()
    {
        var account = new BankAccount(1, "Test", 100m);
        _factoryMock.Setup(f => f.CreateAccount(1, "Test", 100m)).Returns(account);
        
        var result = _service.CreateAccount(1, "Test", 100m);
        
        Assert.Single(_service.BankAccounts);
        Assert.Equal(account, result);
    }

    [Fact]
    public void CreateAccount_WithDuplicateId_ThrowsException()
    {
        _factoryMock.Setup(f => f.CreateAccount(1, "Test", 0m))
            .Returns(new BankAccount(1, "Test"));

        _service.CreateAccount(1, "Test");
        
        Assert.Throws<InvalidOperationException>(() => _service.CreateAccount(1, "Test"));
    }

    [Fact]
    public void Deposit_ValidAmount_UpdatesBalance()
    {
        var account = new BankAccount(1, "Test", 100m);
        _factoryMock.Setup(f => f.CreateAccount(1, "Test", 100m)).Returns(account);
        _service.CreateAccount(1, "Test", 100m);
        
        _service.Deposit(1, 50m);
        
        Assert.Equal(150m, account.Balance);
    }

    [Fact]
    public void GetAccount_NonExistingId_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => _service.GetAccount(999));
    }
}
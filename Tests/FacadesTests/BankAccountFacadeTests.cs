using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.DTO;
using FinanceAccounting.Facades;
using FinanceAccounting.Models;
using FinanceAccounting.Services.Interfaces;
using Moq;
using Xunit;

namespace Tests.FacadesTests;

public class BankAccountFacadeTests
{
    private readonly Mock<IBankAccountService> _bankAccountServiceMock = new();
    private readonly Mock<IDataTransferManager> _dataTransferManagerMock = new();
    private readonly BankAccountFacade _facade;

    public BankAccountFacadeTests()
    {
        _facade = new BankAccountFacade(
            _bankAccountServiceMock.Object,
            _dataTransferManagerMock.Object
        );
    }

    [Fact]
    public void Import_ValidData_ReturnsCreatedAccounts()
    {
        var dtos = new List<BankAccountDto>
        {
            new() { Id = 1, Name = "Account1", Balance = 100 },
            new() { Id = 2, Name = "Account2", Balance = 200 }
        };

        _dataTransferManagerMock.Setup(m => m.Import<BankAccountDto>(It.IsAny<string>()))
            .Returns(dtos);
        
        var result = _facade.Import("test.csv");
        
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void Export_CallsDataTransferManager()
    {
        _facade.Export("test.csv");
        
        _dataTransferManagerMock.Verify(m => 
                m.Export(It.IsAny<string>(), It.IsAny<IReadOnlyCollection<BankAccount>>()), 
            Times.Once);
    }
}
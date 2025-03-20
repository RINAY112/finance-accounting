using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.DTO;
using FinanceAccounting.Facades;
using FinanceAccounting.Services.Interfaces;
using Moq;
using Xunit;

namespace Tests.FacadesTests;

public class OperationFacadeTests
{
    private readonly Mock<IOperationService> _operationServiceMock = new();
    private readonly Mock<IDataTransferManager> _dataTransferManagerMock = new();
    private readonly OperationFacade _facade;

    public OperationFacadeTests()
    {
        _facade = new OperationFacade(
            _operationServiceMock.Object,
            _dataTransferManagerMock.Object
        );
    }

    [Fact]
    public void CreateOperation_ValidDto_CallsServiceWithCorrectParameters()
    {
        var dto = new OperationDto
        {
            Id = 1,
            Amount = 100,
            Date = DateTime.Now,
            BankAccountId = 1,
            CategoryId = 1,
            Description = "Test"
        };
        
        _facade.CreateOperation(dto);
        
        _operationServiceMock.Verify(m => m.CreateOperation(
            dto.Id,
            dto.Amount,
            dto.Date,
            dto.BankAccountId,
            dto.CategoryId,
            dto.Description
        ), Times.Once);
    }
}
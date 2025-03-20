using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.Services;
using Moq;
using Xunit;

namespace Tests.ServicesTests;

public class OperationServiceTests
{
    private readonly Mock<IOperationFactory> _factoryMock = new();
    private readonly OperationService _service;

    public OperationServiceTests()
    {
        _service = new OperationService(_factoryMock.Object);
    }

    [Fact]
    public void CreateOperation_ValidParameters_AddsToCollection()
    {
        var operation = new Operation(1, 100m, DateTime.Now, 1, 1);
        _factoryMock.Setup(f => f.CreateOperation(1, 100m, It.IsAny<DateTime>(), 1, 1, ""))
            .Returns(operation);
        
        var result = _service.CreateOperation(1, 100m, DateTime.Now, 1, 1);
        
        Assert.Single(_service.Operations);
        Assert.Equal(operation, result);
    }

    [Fact]
    public void UpdateOperationDescription_ExistingId_UpdatesDescription()
    {
        var operation = new Operation(1, 100m, DateTime.Now, 1, 1, "Old");
        _factoryMock.Setup(f => f.CreateOperation(1, 100m, It.IsAny<DateTime>(), 1, 1, "Old"))
            .Returns(operation);
        _service.CreateOperation(1, 100m, DateTime.Now, 1, 1, "Old");
        
        _service.UpdateOperationDescription(1, "New");
        
        Assert.Equal("New", operation.Description);
    }

    [Fact]
    public void DeleteOperation_ExistingId_RemovesFromCollection()
    {
        var operation = new Operation(1, 100m, DateTime.Now, 1, 1);
        _factoryMock.Setup(f => f.CreateOperation(1, 100m, It.IsAny<DateTime>(), 1, 1, ""))
            .Returns(operation);
        _service.CreateOperation(1, 100m, DateTime.Now, 1, 1);
        
        _service.DeleteOperation(1);
        
        Assert.Empty(_service.Operations);
    }
}
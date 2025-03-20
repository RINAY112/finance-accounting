using Xunit;
using FinanceAccounting.Factories;
using FinanceAccounting.DTO;

namespace Tests.FactoriesTests;

public class OperationFactoryTests
    {
        private readonly OperationFactory _factory = new();
        private readonly DateTime _testDate = DateTime.Now;

        [Fact]
        public void CreateOperation_WithParameters_ReturnsValidObject()
        {
            var operation = _factory.CreateOperation(1, 100m, _testDate, 123, 456, "Test");
            
            Assert.Equal(1, operation.Id);
            Assert.Equal(100m, operation.Amount);
            Assert.Equal(_testDate, operation.Date);
            Assert.Equal(123, operation.BankAccountId);
            Assert.Equal(456, operation.CategoryId);
            Assert.Equal("Test", operation.Description);
        }

        [Fact]
        public void CreateOperation_FromDto_ReturnsValidObject()
        {
            var dto = new OperationDto
            {
                Id = 2,
                Amount = 200m,
                Date = _testDate,
                BankAccountId = 789,
                CategoryId = 321,
                Description = "DTO Operation"
            };
            
            var operation = _factory.CreateOperation(dto);
            
            Assert.Equal(dto.Id, operation.Id);
            Assert.Equal(dto.Amount, operation.Amount);
            Assert.Equal(dto.Date, operation.Date);
            Assert.Equal(dto.BankAccountId, operation.BankAccountId);
            Assert.Equal(dto.CategoryId, operation.CategoryId);
            Assert.Equal(dto.Description, operation.Description);
        }

        [Fact]
        public void CreateOperation_WithInvalidAmount_ThrowsException()
        {
            var dto = new OperationDto { Amount = -50 };
            
            Assert.Throws<ArgumentException>(() => _factory.CreateOperation(dto));
        }

        [Fact]
        public void CreateOperation_WithNullDescription_ReturnsEmptyString()
        {
            var dto = new OperationDto 
            { 
                Description = null,
                Amount = 100 
            };
            
            var operation = _factory.CreateOperation(dto);
            
            Assert.Equal(string.Empty, operation.Description);
        }
    }
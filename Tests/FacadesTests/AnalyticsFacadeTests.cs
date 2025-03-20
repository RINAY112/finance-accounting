using FinanceAccounting.Facades;
using FinanceAccounting.Models;
using FinanceAccounting.Services.Interfaces;
using Moq;
using Xunit;

namespace Tests.FacadesTests;

public class AnalyticsFacadeTests
    {
        private readonly Mock<ICategoryService> _categoryServiceMock = new();
        private readonly Mock<IBankAccountService> _bankAccountServiceMock = new();
        private readonly Mock<IOperationService> _operationServiceMock = new();
        private readonly AnalyticsFacade _facade;

        public AnalyticsFacadeTests()
        {
            _facade = new AnalyticsFacade(
                _bankAccountServiceMock.Object,
                _categoryServiceMock.Object,
                _operationServiceMock.Object
            );
        }

        [Fact]
        public void CalculatePeriodNetBalance_ValidPeriod_ReturnsCorrectBalance()
        {
            var operations = new List<Operation>
            {
                new(1, 100, DateTime.Now.AddDays(-1), 1, 1),
                new(2, 50, DateTime.Now.AddDays(-1), 1, 2)
            };

            _operationServiceMock.Setup(m => m.Operations).Returns(operations);
            _categoryServiceMock.Setup(m => m.GetCategory(1)).Returns(new Category(1, "Income", CategoryType.Income));
            _categoryServiceMock.Setup(m => m.GetCategory(2)).Returns(new Category(2, "Expense", CategoryType.Expense));
            
            var result = _facade.CalculatePeriodNetBalance(1, DateTime.Now.AddDays(-2), DateTime.Now);
            
            Assert.Equal(100m - 50m, result);
        }

        [Fact]
        public void GetOperationsByType_ReturnsOnlyOperationsWithMatchingCategoryType()
        {
            var operations = new List<Operation>
            {
                new(1, 100, DateTime.Now, 1, 1),
                new(2, 50, DateTime.Now, 1, 2)
            };

            _operationServiceMock.Setup(m => m.Operations).Returns(operations);
            _categoryServiceMock.Setup(m => m.GetCategory(1)).Returns(new Category(1, "Income", CategoryType.Income));
            _categoryServiceMock.Setup(m => m.GetCategory(2)).Returns(new Category(2, "Expense", CategoryType.Expense));
            
            var incomeOperations = _facade.GetOperationsByType(CategoryType.Income);
            
            Assert.Single(incomeOperations);
            Assert.All(incomeOperations, o => Assert.Equal(1, o.CategoryId));
        }
        
        [Fact]
        public void CalculatePeriodNetBalance_EndDateBeforeStart_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => 
                _facade.CalculatePeriodNetBalance(1, DateTime.Now, DateTime.Now.AddDays(-1)));
        }
        
        [Fact]
        public void CalculatePeriodNetBalance_NullEndDate_UsesCurrentDate()
        {
            var testAccount = new BankAccount(1, "Test", 0);
            var testCategory1 = new Category(1, "Income", CategoryType.Income);
            var testCategory2 = new Category(2, "Expense", CategoryType.Expense);
    
            var operations = new List<Operation>
            {
                new(1, 100, DateTime.Now.AddDays(-1), 1, 1, "Income Op"),
                new(2, 50, DateTime.Now.AddDays(1), 1, 2, "Expense Op")
            };
            
            _operationServiceMock.Setup(m => m.Operations).Returns(operations);
            _categoryServiceMock.Setup(m => m.GetCategory(1)).Returns(testCategory1);
            _categoryServiceMock.Setup(m => m.GetCategory(2)).Returns(testCategory2);
            _bankAccountServiceMock.Setup(m => m.GetAccount(1)).Returns(testAccount);
            
            var result = _facade.CalculatePeriodNetBalance(1, DateTime.Now.AddDays(-2));
            
            Assert.Equal(100m, result);
        }
    }
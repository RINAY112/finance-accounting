using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.Services;
using Moq;
using Xunit;

namespace Tests.ServicesTests;

public class CategoryServiceTests
    {
        private readonly Mock<ICategoryFactory> _factoryMock = new();
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _service = new CategoryService(_factoryMock.Object);
        }

        [Fact]
        public void CreateCategory_WithUniqueNameAndType_AddsToCollection()
        {
            var category = new Category(1, "Food", CategoryType.Expense);
            _factoryMock.Setup(f => f.CreateCategory(1, "Food", CategoryType.Expense))
                .Returns(category);
            
            var result = _service.CreateCategory(1, "Food", CategoryType.Expense);
            
            Assert.Single(_service.Categories);
            Assert.Equal(category, result);
        }

        [Fact]
        public void UpdateCategoryName_ToExistingNameAndType_ThrowsException()
        {
            _factoryMock.Setup(f => f.CreateCategory(1, "Food", CategoryType.Expense))
                .Returns(new Category(1, "Food", CategoryType.Expense));
            _factoryMock.Setup(f => f.CreateCategory(2, "Drinks", CategoryType.Expense))
                .Returns(new Category(2, "Drinks", CategoryType.Expense));

            _service.CreateCategory(1, "Food", CategoryType.Expense);
            _service.CreateCategory(2, "Drinks", CategoryType.Expense);
            
            Assert.Throws<InvalidOperationException>(() => 
                _service.UpdateCategoryName(2, "Food"));
        }

        [Fact]
        public void GetCategoriesByType_ReturnsFilteredResults()
        {
            var expense = new Category(1, "Food", CategoryType.Expense);
            var income = new Category(2, "Salary", CategoryType.Income);
            
            _factoryMock.SetupSequence(f => f.CreateCategory(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CategoryType>()))
                .Returns(expense)
                .Returns(income);

            _service.CreateCategory(1, "Food", CategoryType.Expense);
            _service.CreateCategory(2, "Salary", CategoryType.Income);
            
            var result = _service.GetCategoriesByType(CategoryType.Expense);
            
            Assert.Single(result);
            Assert.All(result, c => Assert.Equal(CategoryType.Expense, c.Type));
        }
    }
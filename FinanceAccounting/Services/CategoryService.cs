using FinanceAccounting.DTO;
using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.Services.Interfaces;

namespace FinanceAccounting.Services;

public class CategoryService : ICategoryService
{
    private readonly List<Category> _categories = new List<Category>();
    private readonly ICategoryFactory _factory;
    private int _nextCategoryId = 1;
    
    public IReadOnlyCollection<Category> Categories { get => _categories; }

    public CategoryService(ICategoryFactory factory) => _factory = factory;
    
    public Category CreateCategory(string name, CategoryType type)
    {
        if (_categories.Any(c =>
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                c.Type == type))
            throw new InvalidOperationException($"Category with name '{name}' and type '{type}' already exists.");

        var category = _factory.CreateCategory(_nextCategoryId++, name, type);
        _categories.Add(category);
        return category;
    }
    
    public Category CreateCategory(in CategoryDto dto) => CreateCategory(dto.Name, dto.Type);

    public Category GetCategory(int id)
    {
        return _categories.FirstOrDefault(c => c.Id == id) ??
               throw new ArgumentException($"Category with ID '{id}' not found.");
    }

    public void DeleteCategory(int id)
    {
        var category = GetCategory(id);
        _categories.Remove(category);
    }

    public void UpdateCategoryName(int id, string newName)
    {
        var category = GetCategory(id);
        if (_categories.Any(c => 
                c.Name.Equals(newName, StringComparison.OrdinalIgnoreCase) && 
                c.Type == category.Type && 
                c.Id != id))
            throw new InvalidOperationException($"Category with name '{newName}' and type '{category.Type}' already exists.");
        category.Name = newName;
    }

    public List<Category> GetCategoriesByType(CategoryType type) => _categories.Where(c => c.Type == type).ToList();
}
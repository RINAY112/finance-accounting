using FinanceAccounting.DTO;
using FinanceAccounting.Models;

namespace FinanceAccounting.Services.Interfaces;

public interface ICategoryService
{
    IReadOnlyCollection<Category> Categories { get; }
    
    Category CreateCategory(int id, string name, CategoryType type);
    
    Category CreateCategory(in CategoryDto dto);
    
    void DeleteCategory(int id);

    void UpdateCategoryName(int id, string newName);
    
    Category GetCategory(int id);
    
    List<Category> GetCategoriesByType(CategoryType type);
}
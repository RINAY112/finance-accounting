using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.Services.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.UI;
using FinanceAccounting.DTO;

namespace FinanceAccounting.Facades;

public class CategoryFacade
{
    private readonly ICategoryService _categoryService;
    private readonly IDataTransferManager _dataTransferManager;

    public CategoryFacade(ICategoryService categoryService, IDataTransferManager dataTransferManager)
    {
        _categoryService = categoryService;
        _dataTransferManager = dataTransferManager;
    }
    
    public IReadOnlyCollection<Category> Categories => _categoryService.Categories;

    public Category CreateCategory(int id, string name, CategoryType type) => _categoryService.CreateCategory(id, name, type);

    public Category CreateCategory(in CategoryDto dto) => CreateCategory(dto.Id, dto.Name, dto.Type);

    public void UpdateCategoryName(int id, string newName) => _categoryService.UpdateCategoryName(id, newName);

    public void DeleteCategory(int id) => _categoryService.DeleteCategory(id);

    public List<Category> Import(string filePath)
    {
        var data = _dataTransferManager.Import<CategoryDto>(filePath);
        List<Category> res = new();

        foreach (var item in data)
        {
            try
            {
                res.Add(CreateCategory(item));
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
        }

        return res;
    }

    public void Export(string filePath) => _dataTransferManager.Export(filePath, _categoryService.Categories);
    
    public List<Category> GetCategoriesByType(CategoryType type) => _categoryService.GetCategoriesByType(type);
}
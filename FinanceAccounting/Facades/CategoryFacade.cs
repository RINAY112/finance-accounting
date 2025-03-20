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
}
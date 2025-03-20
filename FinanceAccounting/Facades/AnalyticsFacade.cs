using FinanceAccounting.Models;
using FinanceAccounting.Facades;
using FinanceAccounting.Services.Interfaces;

namespace FinanceAccounting.Facades;

public class AnalyticsFacade
{
    private readonly ICategoryService _categoryService;
    private readonly IBankAccountService _bankAccountService;
    private readonly IOperationService _operationService;

    public AnalyticsFacade(IBankAccountService bankAccountService, ICategoryService categoryService, IOperationService operationService)
    {
        _categoryService = categoryService;
        _bankAccountService = bankAccountService;
        _operationService = operationService;
    }

    public List<Operation> GetOperationsByType(CategoryType type)
    {
        List<Operation> res = new();
        foreach (var operation in _operationService.Operations)
        {
            try
            {
                if (_categoryService.GetCategory(operation.CategoryId).Type == type)
                    res.Add(operation);
            }
            catch (Exception) {}
        }

        return res;
    }

    public decimal CalculatePeriodNetBalance(int bankAccountId, DateTime startDate, DateTime? endDate = null)
    {
        endDate ??= DateTime.Now;
        
        if (endDate < startDate) 
            throw new ArgumentException("End date must be greater than start date");
        
        var account = _bankAccountService.GetAccount(bankAccountId);
        
        var a = GetOperationsByType(CategoryType.Income)
                    .Where(
                        o => o.BankAccountId == bankAccountId
                             && o.Date >= startDate
                             && o.Date <= endDate
                    )
                    .Sum(o => o.Amount) -
                GetOperationsByType(CategoryType.Expense)
                    .Where(
                        o => o.BankAccountId == bankAccountId
                             && o.Date >= startDate
                             && o.Date <= endDate
                    )
                    .Sum(o => o.Amount);

        return GetOperationsByType(CategoryType.Income)
                   .Where(
                       o => o.BankAccountId == bankAccountId
                            && o.Date >= startDate
                            && o.Date <= endDate
                   )
                   .Sum(o => o.Amount) -
               GetOperationsByType(CategoryType.Expense)
                   .Where(
                       o => o.BankAccountId == bankAccountId
                            && o.Date >= startDate
                            && o.Date <= endDate
                   )
                   .Sum(o => o.Amount);
    }
}
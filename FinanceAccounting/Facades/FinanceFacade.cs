using FinanceAccounting.Models;
using FinanceAccounting.Facades;
using FinanceAccounting.Services;

namespace FinanceAccounting.Facades;

public class FinanceFacade
{
    private readonly CategoryFacade _categoryFacade;
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly OperationFacade _operationFacade;

    public FinanceFacade(CategoryFacade categoryFacade, BankAccountFacade bankAccountFacade, OperationFacade operationFacade)
    {
        _categoryFacade = categoryFacade;
        _bankAccountFacade = bankAccountFacade;
        _operationFacade = operationFacade;
    }
}
using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.Services;
using FinanceAccounting.Factories;

namespace FinanceAccounting.Facades;

public class OperationFacade
{
    private readonly OperationService _operationService;
    private readonly IDataTransferManager _dataTransferManager;

    public OperationFacade(OperationService operationService, IDataTransferManager dataTransferManager)
    {
        _operationService = operationService;
        _dataTransferManager = dataTransferManager;
    }
}
using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.Services.Interfaces;
using FinanceAccounting.DTO;
using FinanceAccounting.UI;

namespace FinanceAccounting.Facades;

public class OperationFacade
{
    private readonly IOperationService _operationService;
    private readonly IDataTransferManager _dataTransferManager;

    public OperationFacade(IOperationService operationService, IDataTransferManager dataTransferManager)
    {
        _operationService = operationService;
        _dataTransferManager = dataTransferManager;
    }
    
    public IReadOnlyCollection<Operation> Operations => _operationService.Operations;

    public Operation CreateOperation(int id, decimal amount, DateTime date, int bankAccountId, int categoryId,
        string description)
    {
        return _operationService.CreateOperation(id, amount, date, bankAccountId, categoryId, description);
    }

    public Operation CreateOperation(in OperationDto dto)
    {
        return CreateOperation(dto.Id, dto.Amount, dto.Date, dto.BankAccountId, dto.CategoryId, dto.Description);
    }
    
    public void UpdateOperationDescription(int id, string newDescription) => 
        _operationService.UpdateOperationDescription(id, newDescription);

    public List<Operation> Import(string filePath)
    {
        var data = _dataTransferManager.Import<OperationDto>(filePath);
        List<Operation> res = new();
        foreach (var item in data)
        {
            try
            {
                res.Add(CreateOperation(item));
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
        }

        return res;
    }
    
    public void DeleteOperation(int id) => _operationService.DeleteOperation(id);

    public void Export(string filePath) => _dataTransferManager.Export(filePath, _operationService.Operations);
}
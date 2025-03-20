using FinanceAccounting.DTO;
using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.Services.Interfaces;

namespace FinanceAccounting.Services;

public class OperationService : IOperationService
{
    private readonly List<Operation> _operations = new List<Operation>();
    private int _nextOperationId = 1;
    public IReadOnlyCollection<Operation> Operations { get => _operations; }
    
    private IOperationFactory _factory;
    
    public OperationService(IOperationFactory factory) => _factory = factory;

    public Operation CreateOperation(decimal amount, DateTime date, int bankAccountId, int categoryId, string description = "")
    {
        var operation = new Operation(_nextOperationId++, amount, date, bankAccountId, categoryId, description);
        _operations.Add(operation);
        return operation;
    }

    public Operation CreateOperation(in OperationDto dto)
    {
        return CreateOperation(dto.Amount, dto.Date, dto.BankAccountId, dto.CategoryId, dto.Description);
    }

    public Operation GetOperation(int id)
    {
        return _operations.FirstOrDefault(o => o.Id == id) ??
               throw new ArgumentException($"Operation with ID '{id}' not found.");
    }

    public void UpdateOperationDescription(int id, string newDescription)
    {
        var operation = GetOperation(id);
        operation.Description = newDescription;
    }
    
    public void DeleteOperation(int id)
    {
        var operation = GetOperation(id);
        _operations.Remove(operation);
    }
}
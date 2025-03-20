using FinanceAccounting.DTO;
using FinanceAccounting.Models;

namespace FinanceAccounting.Services.Interfaces;

public interface IOperationService
{
    IReadOnlyCollection<Operation> Operations { get; }
    
    Operation CreateOperation(decimal amount, DateTime date, int accountId, int categoryId, string description = "");
    
    Operation CreateOperation(in OperationDto dto);
    
    void DeleteOperation(int id);
    
    void UpdateOperationDescription(int id, string newDescription);
    
    Operation GetOperation(int id);
}
using FinanceAccounting.DTO;
using FinanceAccounting.Models;
namespace FinanceAccounting.Factories.Interfaces;

public interface IOperationFactory
{
    Operation CreateOperation(int id, decimal amount, DateTime date, int bankAccountId, int categoryId, string description);
    
    Operation CreateOperation(in OperationDto dto);
}
using FinanceAccounting.DTO;
using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;

namespace FinanceAccounting.Factories;

public class OperationFactory : IOperationFactory
{
    public Operation CreateOperation(int id, decimal amount, DateTime date, int bankAccountId, int categoryId, string description)
    {
        return new Operation(id, amount, date, bankAccountId, categoryId, description);
    }

    public Operation CreateOperation(in OperationDto dto)
    {
        return new Operation(dto.Id, dto.Amount, dto.Date, dto.BankAccountId, dto.CategoryId, dto.Description);
    }
}
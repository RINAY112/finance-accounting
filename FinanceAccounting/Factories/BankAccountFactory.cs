using FinanceAccounting.DTO;
using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;

namespace FinanceAccounting.Factories;

public class BankAccountFactory : IBankAccountFactory
{
    public BankAccount CreateAccount(int id, string name, decimal balance = 0)
    {
        return new BankAccount(id, name, balance);
    }

    public BankAccount CreateAccount(in BankAccountDto dto)
    {
        return new BankAccount(dto.Id, dto.Name, dto.Balance);
    }
}
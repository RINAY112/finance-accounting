using FinanceAccounting.DTO;
using FinanceAccounting.Models;

namespace FinanceAccounting.Factories.Interfaces;

public interface IBankAccountFactory
{
    BankAccount CreateAccount(int id, string name, decimal balance);
    
    BankAccount CreateAccount(in BankAccountDto dto);
}
using FinanceAccounting.DTO;
using FinanceAccounting.Models;

namespace FinanceAccounting.Services.Interfaces;

public interface IBankAccountService
{
    IReadOnlyCollection<BankAccount> BankAccounts { get; }
    
    BankAccount CreateAccount(string name, decimal initialBalance = 0);
    
    BankAccount CreateAccount(in BankAccountDto dto);
    
    void DeleteAccount(int id);
    
    void UpdateAccountName(int id, string newName);
    
    BankAccount GetAccount(int id);
    
    void Deposit(int id, decimal amount);
    
    void Withdraw(int id, decimal amount);
}
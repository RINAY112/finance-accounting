using FinanceAccounting.DTO;
using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.Services.Interfaces;

namespace FinanceAccounting.Services;

public class BankAccountService : IBankAccountService
{
    private readonly List<BankAccount> _bankAccounts = new();
    private readonly IBankAccountFactory _factory;
    
    public IReadOnlyCollection<BankAccount> BankAccounts => _bankAccounts; 

    public BankAccountService(IBankAccountFactory factory) => _factory = factory;

    public BankAccount CreateAccount(int id, string name, decimal balance = 0)
    {
        if (_bankAccounts.Any(b => b.Id == id))
            throw new InvalidOperationException($"Account with ID '{id}' already exists.");
        var account = _factory.CreateAccount(id, name, balance);
        _bankAccounts.Add(account);
        return account;
    }

    public BankAccount CreateAccount(in BankAccountDto dto) => CreateAccount(dto.Id, dto.Name, dto.Balance);

    public void UpdateAccountName(int id, string newName)
    {
        var account = GetAccount(id);
        account.Name = newName;
    }

    public BankAccount GetAccount(int id)
    {
        return _bankAccounts.FirstOrDefault(account => account.Id == id) ??
               throw new ArgumentException($"Bank account with ID '{id}' not found.");
    }

    public void Deposit(int accountId, decimal amount)
    {
        var account = GetAccount(accountId);
        account.Deposit(amount);
    }

    public void Withdraw(int accountId, decimal amount)
    {
        var account = GetAccount(accountId);
        account.Withdraw(amount);
    }
    
    public void DeleteAccount(int accountId)
    {
        var accountToRemove = GetAccount(accountId);
        _bankAccounts.Remove(accountToRemove);
    }
}
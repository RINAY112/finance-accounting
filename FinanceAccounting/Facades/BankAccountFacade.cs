using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.Services.Interfaces;
using FinanceAccounting.Models;
using FinanceAccounting.UI;
using FinanceAccounting.DTO;

namespace FinanceAccounting.Facades;

public class BankAccountFacade
{
    private readonly IBankAccountService _bankAccountService;
    private readonly IDataTransferManager _dataTransferManager;

    public BankAccountFacade(IBankAccountService bankAccountService, IDataTransferManager dataTransferManager)
    {
        _bankAccountService = bankAccountService;
        _dataTransferManager = dataTransferManager;
    }

    public void Import(string filePath)
    {
        var data = _dataTransferManager.Import<BankAccountDto>(filePath);
        foreach (var item in data)
        {
            CreateAccount(item);
        }
    }

    public void Export(string filePath) => _dataTransferManager.Export<BankAccount>(filePath, _bankAccountService.BankAccounts);
    
    public void CreateAccount(string name, decimal balance = 0) => _bankAccountService.CreateAccount(name, balance);
    
    public void CreateAccount(in BankAccountDto dto) => CreateAccount(dto.Name, dto.Balance);
    
    public void UpdateAccountName(int accountId, in string newName) => _bankAccountService.UpdateAccountName(accountId, newName);
    
    public void DepositToAccount(int accountId, decimal amount) => _bankAccountService.Deposit(accountId, amount);
    
    public void WithdrawFromAccount(int accountId, decimal amount) => _bankAccountService.Withdraw(accountId, amount);

    public void DeleteAccount(int accountId) => _bankAccountService.DeleteAccount(accountId);
}
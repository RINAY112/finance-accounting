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
    
    public IReadOnlyCollection<BankAccount> BankAccounts => _bankAccountService.BankAccounts;

    public List<BankAccount> Import(string filePath)
    {
        var data = _dataTransferManager.Import<BankAccountDto>(filePath);
        List<BankAccount> res = new();
        foreach (var item in data)
        {
            try
            {
                res.Add(CreateAccount(item));
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError(ex.Message);
            }
        }

        return res;
    }

    public void Export(string filePath) => _dataTransferManager.Export<BankAccount>(filePath, _bankAccountService.BankAccounts);
    
    public BankAccount CreateAccount(int id, string name, decimal balance = 0) => _bankAccountService.CreateAccount(id, name, balance);
    
    public BankAccount CreateAccount(in BankAccountDto dto) => CreateAccount(dto.Id, dto.Name, dto.Balance);
    
    public void UpdateAccountName(int accountId, string newName) => _bankAccountService.UpdateAccountName(accountId, newName);
    
    public void DepositToAccount(int accountId, decimal amount) => _bankAccountService.Deposit(accountId, amount);
    
    public void WithdrawFromAccount(int accountId, decimal amount) => _bankAccountService.Withdraw(accountId, amount);

    public void DeleteAccount(int accountId) => _bankAccountService.DeleteAccount(accountId);
}
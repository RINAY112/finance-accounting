namespace FinanceAccounting.Models;

public class BankAccount
{
    private int _id;
    public int Id => _id;

    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Account name cannot be empty.");
            _name = value;
        }
    }

    private decimal _balance;
    public decimal Balance => _balance;

    public BankAccount(int id, string name, decimal balance = 0)
    {
        Name = name;
        _balance = balance;
        _id = id;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive.", nameof(amount));
        }

        _balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive.", nameof(amount));
        }
        if (Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds in the account.");
        }
        _balance -= amount;
    }
}
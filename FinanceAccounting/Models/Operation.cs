namespace FinanceAccounting.Models;

public class Operation
{
    private int _id;
    public int Id => _id;
    
    private decimal _amount;
    public decimal Amount => _amount;

    private DateTime _date;
    public DateTime Date => _date;
    
    private string _description;
    
    private int _bankAccountId;
    public int BankAccountId  => _bankAccountId;
    
    private int _categoryId;
    public int CategoryId => _categoryId;
    
    public string Description
    {
        get => _description;
        set => _description = value ?? string.Empty;
    }

    public Operation(int id, decimal amount, DateTime date, int bankAccountId, int categoryId, string description = "")
    {
        if (amount <= 0)
            throw new ArgumentException("Operation amount must be positive.", nameof(amount));
        _amount = amount;
        _date = date;
        _bankAccountId = bankAccountId;
        _categoryId = categoryId;
        Description = description;
        _id = id;
    }

    public override string ToString() => $"ID: {_id} | Amount: {_amount} | Creation date: {_date} | " +
                                         $"Account ID: {_bankAccountId} | Category ID: {_categoryId}" +
                                         $"\nDescription:\n\"{_description}\"";
}
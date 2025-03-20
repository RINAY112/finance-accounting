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

    public string Description
    {
        get => _description;
        set => _description = value ?? string.Empty;
    }
    
    private int _bankAccountId;
    public int BankAccountId  => _bankAccountId;
    
    private int _categoryId;
    public int CategoryId => _categoryId;

    public Operation(int id, decimal amount, DateTime date, int bankAccountId, int categoryId, string description = "")
    {
        if (amount <= 0)
            throw new ArgumentException("Operation amount must be positive.", nameof(amount));
        _amount = amount;
        _date = date;
        _bankAccountId = bankAccountId;
        _categoryId = categoryId;
        _description = description;
        _id = id;
    }

}
namespace FinanceAccounting.Models;

public enum CategoryType
{
    Income,
    Expense
}

public class Category
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
                throw new ArgumentException("Category name cannot be empty.");
            _name = value;
        }
    }
    
    private CategoryType _type;
    public CategoryType Type => _type;

    public Category(int id, string name, CategoryType type)
    {
        _id = id;
        Name = name;
        _type = type;
    }
}
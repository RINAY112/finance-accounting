using FinanceAccounting.Models;

namespace FinanceAccounting.DTO;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
}
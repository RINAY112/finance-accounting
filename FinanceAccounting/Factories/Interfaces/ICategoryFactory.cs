using FinanceAccounting.DTO;
using FinanceAccounting.Models;

namespace FinanceAccounting.Factories.Interfaces;

public interface ICategoryFactory
{
    Category CreateCategory(int id, string name, CategoryType type);
    
    Category CreateCategory(in CategoryDto dto);
}
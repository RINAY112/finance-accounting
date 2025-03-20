using FinanceAccounting.DTO;
using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Models;

namespace FinanceAccounting.Factories;

public class CategoryFactory : ICategoryFactory
{
    public Category CreateCategory(int id, string name, CategoryType type)
    {
        return new Category(id, name, type);
    }

    public Category CreateCategory(in CategoryDto dto)
    {
        return new Category(dto.Id, dto.Name, dto.Type);
    }
}
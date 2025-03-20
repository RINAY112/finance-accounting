using CsvHelper.Configuration;
using FinanceAccounting.Data.Converters;
using FinanceAccounting.Models;

namespace FinanceAccounting.Data.ClassMaps;

public sealed class CategoryMap : ClassMap<Category>
{
    public CategoryMap()
    {
        Map(m => m.Id).Name("Id");
        Map(m => m.Name).Name("Name");
        Map(m => m.Type).Name("Type").TypeConverter<CsvEnumConverter<CategoryType>>();
    }
}

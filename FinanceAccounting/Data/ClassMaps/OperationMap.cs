using CsvHelper.Configuration;
using FinanceAccounting.Data.Converters;
using FinanceAccounting.Models;

namespace FinanceAccounting.Data.ClassMaps;

public sealed class OperationMap : ClassMap<Operation>
{
    public OperationMap()
    {
        Map(m => m.Id).Name("Id");
        Map(m => m.Amount).Name("Amount");
        Map(m => m.Date).Name("Date").TypeConverter(new CsvDateTimeConverter());
        Map(m => m.BankAccountId).Name("BankAccountId");
        Map(m => m.CategoryId).Name("CategoryId");
        Map(m => m.Description).Name("Description");
    }
}
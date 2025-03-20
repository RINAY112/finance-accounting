using CsvHelper.Configuration;
using FinanceAccounting.Models;

namespace FinanceAccounting.Data.ClassMaps;

public sealed class BankAccountMap : ClassMap<BankAccount>
{
    public BankAccountMap()
    {
        Map(m => m.Id).Name("Id");
        Map(m => m.Name).Name("Name");
        Map(m => m.Balance).Name("Balance");
    }
}
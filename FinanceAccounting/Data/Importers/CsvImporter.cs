using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using FinanceAccounting.Data.ClassMaps;

namespace FinanceAccounting.Data.Importers;

public class CsvImporter : DataImporter
{
    protected override List<T> Read<T>(StreamReader reader)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true
        };
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<CategoryMap>();
            csv.Context.RegisterClassMap<BankAccountMap>();
            csv.Context.RegisterClassMap<OperationMap>();
            return csv.GetRecords<T>().ToList();
        }
    }
}
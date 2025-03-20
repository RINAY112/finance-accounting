using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FinanceAccounting.Data.ClassMaps;

namespace FinanceAccounting.Data.Exporters;

public class CsvExporter : DataExporter
{
    
    public override string Format => ".csv";
    
    protected override void Write<T>(StreamWriter writer, IReadOnlyCollection<T> data)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };
        using (var csv = new CsvWriter(writer, config))
        {
            csv.Context.RegisterClassMap<CategoryMap>();
            csv.Context.RegisterClassMap<BankAccountMap>();
            csv.Context.RegisterClassMap<OperationMap>();
            csv.WriteHeader<T>();
            csv.NextRecord();
            csv.WriteRecords(data);
        }
    }
} 
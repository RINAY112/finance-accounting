using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using FinanceAccounting.Data.ClassMaps;
using FinanceAccounting.DTO;
using Xunit;

namespace Tests.DataTests.ConvertersAndMapsTests;

public class CsvMapTests
{
    [Fact]
    public void BankAccountMap_ReadsCorrectValues()
    {
        var csvContent = new StringBuilder()
            .AppendLine("Id;Name;Balance")
            .AppendLine("1;Test Account;1500.75")
            .ToString();

        using var reader = new StringReader(csvContent);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        });
        
        csv.Context.RegisterClassMap<BankAccountMap>();
        var records = csv.GetRecords<BankAccountDto>().ToList();
        
        var account = records.First();
        Assert.Equal(1, account.Id);
        Assert.Equal("Test Account", account.Name);
        Assert.Equal(1500.75m, account.Balance);
    }
    
    [Fact]
    public void OperationMap_ShouldHandleDateTime()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = false,
        };
        using var reader = new StringReader("1,100,2023-10-05 14:30:15,1,1,");
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<OperationMap>();

        csv.Read();
        var record = csv.GetRecord<OperationDto>();
            
        Assert.Equal(new DateTime(2023, 10, 5, 14, 30, 15), record.Date);
    }
}
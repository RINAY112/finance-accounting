using FinanceAccounting.Data.Exporters;
using FinanceAccounting.Models;
using Xunit;

namespace Tests.DataTests.ExporterTests;

public class CsvExporterTests
{
    [Fact]
    public void Export_ShouldGenerateValidCsv()
    {
        var exporter = new CsvExporter();
        var accounts = new List<BankAccount>
        {
            new BankAccount(1, "Account1", 100.0m),
            new BankAccount(2, "Account2", 200.0m)
        };

        using var tempFile = new TempFile(".csv");
        
        exporter.Export(tempFile.Path, accounts);
        
        var lines = File.ReadAllLines(tempFile.Path);
        Assert.Equal("Id;Name;Balance", lines[0]);
        Assert.Equal("1;Account1;100.0", lines[1]);
        Assert.Equal("2;Account2;200.0", lines[2]);
    }
}
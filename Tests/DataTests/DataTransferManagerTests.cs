using FinanceAccounting.Data.Exporters;
using FinanceAccounting.Data.Importers;
using FinanceAccounting.Data.Managers;
using FinanceAccounting.Models;
using FinanceAccounting.DTO;
using Xunit;

namespace Tests.DataTests;

public class DataTransferManagerTests
{
    [Fact]
    public void RoundTrip_Csv_ShouldPreserveData()
    {
        var manager = new DataTransferManager();
        manager.RegisterImporter(new CsvImporter());
        manager.RegisterExporter(new CsvExporter());

        var originalData = new List<BankAccount>
        {
            new BankAccount(1, "Test", 100m)
        };

        using var tempFile = new TempFile(".csv");
        
        manager.Export(tempFile.Path, originalData);
        var importedData = manager.Import<BankAccountDto>(tempFile.Path);
        
        Assert.Equal(originalData.First().Name, importedData.First().Name);
    }

    [Fact]
    public void Import_UnsupportedFormat_ShouldThrow()
    {
        var manager = new DataTransferManager();
        using var tempFile = new TempFile(".xml");
        
        Assert.Throws<NotSupportedException>(() => 
            manager.Import<BankAccountDto>(tempFile.Path));
    }
}
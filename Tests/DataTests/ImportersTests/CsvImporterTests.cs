using System.Text;
using FinanceAccounting.Data.Importers;
using FinanceAccounting.DTO;
using FinanceAccounting.Models;
using Xunit;

namespace Tests.DataTests.ImportersTests;

public class CsvImporterTests
{
    [Fact]
    public void Import_ShouldReadMappedData()
    {
        var csvContent = new StringBuilder()
            .AppendLine("Id;Name;Balance")
            .AppendLine("1;Test Account;1500.75")
            .ToString();

        using var tempFile = new TempFile(".csv");
        File.WriteAllText(tempFile.Path, csvContent);
        var importer = new CsvImporter();
        
        var accounts = importer.Import<BankAccountDto>(tempFile.Path);
        
        var account = accounts.First();
        Assert.Equal(1, account.Id);
        Assert.Equal("Test Account", account.Name);
        Assert.Equal(1500.75m, account.Balance);
    }
}
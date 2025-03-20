using Xunit;
using FinanceAccounting.Data.Importers;
using FinanceAccounting.DTO;
using FinanceAccounting.Models;

namespace Tests.DataTests.ImportersTests;

public class YamlImporterTests
{
    [Fact]
    public void YamlImporter_ReadsDecimalCorrectly()
    {
        var yamlContent = """
                          - Id: 1
                            Amount: 150.75
                            Date: '2023-10-05 14:30:00'
                            BankAccountId: 1
                            CategoryId: 2
                          """;
        using var tempFile = new TempFile(".yaml");
        File.WriteAllText(tempFile.Path, yamlContent);
        var importer = new YamlImporter();
        
        var operations = importer.Import<OperationDto>(tempFile.Path);
        
        var operation = operations.First();
        Assert.Equal(150.75m, operation.Amount);
        Assert.Equal(new DateTime(2023, 10, 5, 14, 30, 0), operation.Date);
    }
}
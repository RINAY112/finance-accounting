using FinanceAccounting.Data.Importers;
using FinanceAccounting.DTO;
using FinanceAccounting.Models;
using Xunit;

namespace Tests.DataTests.ImportersTests;

public class JsonImporterTests
{
    [Fact]
    public void Import_ShouldDeserializeComplexObjects()
    {
        var json = """
                   [
                       {
                           "Id": 1,
                           "Amount": 100.50,
                           "Date": "2023-10-05 14:30:00",
                           "BankAccountId": 1,
                           "CategoryId": 2
                       }
                   ]
                   """;

        using var tempFile = new TempFile(".json");
        File.WriteAllText(tempFile.Path, json);
        var importer = new JsonImporter();
        
        var operations = importer.Import<OperationDto>(tempFile.Path);
        
        var operation = operations.First();
        Assert.Equal(100.50m, operation.Amount);
        Assert.Equal(new DateTime(2023, 10, 5, 14, 30, 0), operation.Date);
    }
}
using FinanceAccounting.Data.Exporters;
using FinanceAccounting.Models;
using Xunit;

namespace Tests.DataTests.ExporterTests;

public class JsonExporterTests
{
    [Fact]
    public void Export_ShouldSerializeDateTimeCorrectly()
    {
        var exporter = new JsonExporter();
        var operation = new Operation(1, 100m, new DateTime(2023, 10, 5), 1, 1);
        using var tempFile = new TempFile(".json");
        
        exporter.Export(tempFile.Path, new[] { operation });
        
        var json = File.ReadAllText(tempFile.Path);
        Assert.Contains("\"Date\": \"2023-10-05 00:00:00\"", json);
    }
}
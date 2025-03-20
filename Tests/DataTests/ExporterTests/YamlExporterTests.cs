using FinanceAccounting.Data.Exporters;
using FinanceAccounting.Models;
using Xunit;

namespace Tests.DataTests.ExporterTests;

public class YamlExporterTests
{
    [Fact]
    public void Export_ShouldUseCustomConverters()
    {
        var exporter = new YamlExporter();
        var category = new Category(1, "Food", CategoryType.Expense);
        using var tempFile = new TempFile(".yaml");
        
        exporter.Export(tempFile.Path, new[] { category });
        
        var yaml = File.ReadAllText(tempFile.Path);
        Assert.Contains("Type: Expense", yaml);
    }
}
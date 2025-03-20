using FinanceAccounting.Data.Converters;
using FinanceAccounting.Models;
using Xunit;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Tests.DataTests.ConvertersAndMapsTests;

public class YamlEnumConverterTests
{
    [Fact]
    public void ReadYaml_ValidValue_ReturnsEnum()
    {
        var yaml = "value: Expense";
        using var reader = new StringReader(yaml);
        var parser = new Parser(reader);
        
        parser.Consume<StreamStart>();
        parser.Consume<DocumentStart>();
        parser.Consume<MappingStart>();
        parser.Consume<Scalar>(); // Пропускаем ключ "value"
        
        var converter = new YamlEnumConverter<CategoryType>();
        var result = converter.ReadYaml(parser, typeof(CategoryType), null!);
        
        Assert.Equal(CategoryType.Expense, result);
    }

    [Fact]
    public void WriteYaml_Enum_WritesString()
    {
        var converter = new YamlEnumConverter<CategoryType>();
        var emitter = new MockEmitter();
        converter.WriteYaml(emitter, CategoryType.Income, typeof(CategoryType), null!);
        Assert.Equal("Income", emitter.EmittedValue);
    }
}
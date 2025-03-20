using FinanceAccounting.Data.Converters;
using Xunit;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Tests.DataTests.ConvertersAndMapsTests;

public class YamlDecimalConverterTests
{
    private readonly YamlDecimalConverter _converter = new();

    [Theory]
    [InlineData("123.45", 123.45)]
    [InlineData("1000", 1000)]
    [InlineData("0.01", 0.01)]
    public void ReadYaml_ValidString_ReturnsDecimal(string input, decimal expected)
    {
        var yaml = $"value: {input}";
        using var reader = new StringReader(yaml);
        var parser = new Parser(reader);
        
        parser.Consume<StreamStart>();
        parser.Consume<DocumentStart>();
        parser.Consume<MappingStart>();
        parser.Consume<Scalar>(); // Пропускаем ключ "value"
        
        var result = _converter.ReadYaml(parser, typeof(decimal), null!);
        
        Assert.Equal(expected, result);
    }

    [Fact]
    public void WriteYaml_Decimal_WritesInvariantString()
    {
        var emitter = new MockEmitter();
        _converter.WriteYaml(emitter, 1234.56m, typeof(decimal), null!);
        Assert.Equal("1234.56", emitter.EmittedValue);
    }
}
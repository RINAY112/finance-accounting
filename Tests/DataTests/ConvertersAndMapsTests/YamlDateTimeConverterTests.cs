using FinanceAccounting.Data.Converters;
using Xunit;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Tests.DataTests.ConvertersAndMapsTests;

public class YamlDateTimeConverterTests
{
    private readonly YamlDateTimeConverter _converter = new();

    [Fact]
    public void ReadYaml_ValidValue_ReturnsDateTime()
    {
        var parser = new Parser(new StringReader("date: '2023-10-05 14:30:15'"));
        parser.Consume<StreamStart>();
        parser.Consume<DocumentStart>();
        parser.Consume<MappingStart>();
        parser.Consume<Scalar>();
        var result = _converter.ReadYaml(parser, typeof(DateTime), null!);
        Assert.Equal(new DateTime(2023, 10, 5, 14, 30, 15), result);
    }

    [Fact]
    public void WriteYaml_DateTime_WritesFormattedString()
    {
        var emitter = new MockEmitter();
        var date = new DateTime(2023, 10, 5, 14, 30, 15);
        var converter = new YamlDateTimeConverter();
        
        converter.WriteYaml(emitter, date, typeof(DateTime), null!);
        
        Assert.Equal("2023-10-05 14:30:15", emitter.EmittedValue);
    }
}
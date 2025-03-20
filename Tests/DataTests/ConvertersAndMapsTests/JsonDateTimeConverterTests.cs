using System.Text;
using System.Text.Json;
using FinanceAccounting.Data.Converters;
using Xunit;

namespace Tests.DataTests.ConvertersAndMapsTests;

public class JsonDateTimeConverterTests
{
    private readonly JsonDateTimeConverter _converter = new();

    [Fact]
    public void Read_ValidString_ReturnsDateTime()
    {
        var reader = new Utf8JsonReader("\"2023-10-05 14:30:15\""u8);
        reader.Read();
        var result = _converter.Read(ref reader, typeof(DateTime), null);
        Assert.Equal(new DateTime(2023, 10, 5, 14, 30, 15), result);
    }

    [Fact]
    public void Write_DateTime_WritesFormattedString()
    {
        var date = new DateTime(2023, 10, 5, 14, 30, 15);
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);

        _converter.Write(writer, date, null);
        writer.Flush();
            
        var result = Encoding.UTF8.GetString(stream.ToArray());
        Assert.Equal("\"2023-10-05 14:30:15\"", result);
    }
}
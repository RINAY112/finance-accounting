using System.Text.Json;
using System.Text.Json.Serialization;
using FinanceAccounting.Data.Converters;

namespace FinanceAccounting.Data.Exporters;

public class JsonExporter : DataExporter
{
    public override string Format => ".json";
    
    protected override void Write<T>(StreamWriter writer, IReadOnlyCollection<T> data)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(),
                new JsonDateTimeConverter()
            }
        };
        string jsonString = JsonSerializer.Serialize(data, options);
        writer.Write(jsonString);
    }
}
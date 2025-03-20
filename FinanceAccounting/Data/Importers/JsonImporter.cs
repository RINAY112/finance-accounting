using System.Text.Json;
using System.Text.Json.Serialization;
using FinanceAccounting.Data.Converters;

namespace FinanceAccounting.Data.Importers;

public class JsonImporter : DataImporter
{
    protected override List<T> Read<T>(StreamReader reader)
    {
        string jsonString = reader.ReadToEnd();
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(),
                new JsonDateTimeConverter()
            },
            PropertyNameCaseInsensitive = false
        };
        var data = JsonSerializer.Deserialize<List<T>>(jsonString, options);
        return data ?? new List<T>();
    }
}
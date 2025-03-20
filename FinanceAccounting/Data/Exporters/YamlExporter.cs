using YamlDotNet.Serialization;
using FinanceAccounting.Data.Converters;
using FinanceAccounting.Models;

namespace FinanceAccounting.Data.Exporters;

public class YamlExporter : DataExporter
{
    protected override void Write<T>(StreamWriter writer, IReadOnlyCollection<T> data)
    {
        var serializer = new SerializerBuilder()
            .WithTypeConverter(new YamlEnumConverter<CategoryType>())
            .WithTypeConverter(new YamlDateTimeConverter())
            .WithTypeConverter(new YamlDecimalConverter())
            .Build();
        serializer.Serialize(writer, data);
    }
}
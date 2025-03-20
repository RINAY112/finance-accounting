using YamlDotNet.Serialization;
using FinanceAccounting.Data.Converters;
using FinanceAccounting.Models;

namespace FinanceAccounting.Data.Importers;

public class YamlImporter : DataImporter
{
    public override string Format => ".yaml";
    
    protected override List<T> Read<T>(StreamReader reader)
    {
        var deserializer = new DeserializerBuilder()
            .WithTypeConverter(new YamlEnumConverter<CategoryType>())
            .WithTypeConverter(new YamlDateTimeConverter())
            .WithTypeConverter(new YamlDecimalConverter())
            .Build();
        return deserializer.Deserialize<List<T>>(reader);
    }
}
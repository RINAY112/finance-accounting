using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using System.Globalization;

namespace FinanceAccounting.Data.Converters;

public class YamlDecimalConverter : IYamlTypeConverter
{
    public bool Accepts(Type type) => type == typeof(decimal);

    public object ReadYaml(IParser parser, Type type, ObjectDeserializer nestedObjectDeserializer)
    {
        var scalar = parser.Consume<Scalar>();
        return decimal.Parse(scalar.Value, CultureInfo.InvariantCulture);
    }

    public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer nestedObjectSerializer)
    {
        var decimalValue = (decimal)value;
        emitter.Emit(new Scalar(decimalValue.ToString(CultureInfo.InvariantCulture)));
    }
}
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FinanceAccounting.Data.Converters;

public class YamlEnumConverter<T> : IYamlTypeConverter where T : struct, Enum
{
    public bool Accepts(Type type) => type == typeof(T);

    public object ReadYaml(IParser parser, Type type, ObjectDeserializer nestedObjectDeserializer)
    {
        var scalar = parser.Consume<Scalar>();
        return Enum.Parse<T>(scalar.Value);
    }

    public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer nestedObjectSerializer)
    {
        emitter.Emit(new Scalar(value.ToString()));
    }
}
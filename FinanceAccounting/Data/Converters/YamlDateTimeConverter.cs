using System.Globalization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FinanceAccounting.Data.Converters;

public class YamlDateTimeConverter : IYamlTypeConverter
{
    private const string Format = "yyyy-MM-dd HH:mm:ss";
    
    public bool Accepts(Type type) => type == typeof(DateTime);

    public object ReadYaml(IParser parser, Type type, ObjectDeserializer nestedObjectDeserializer)
    {
        var scalar = parser.Consume<Scalar>();
        return DateTime.ParseExact(scalar.Value, Format, CultureInfo.InvariantCulture);
    }

    public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer nestedObjectSerializer)
    {
        var date = (DateTime)value;
        emitter.Emit(new Scalar(
            anchor: null,
            tag: null,
            value: date.ToString(Format, CultureInfo.InvariantCulture),
            style: ScalarStyle.SingleQuoted,
            isPlainImplicit: false,
            isQuotedImplicit: true 
        ));
    }
}
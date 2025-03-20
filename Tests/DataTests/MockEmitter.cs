using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Tests.DataTests;

public class MockEmitter : IEmitter
{
    public string EmittedValue { get; private set; } = string.Empty;

    public void Emit(ParsingEvent @event)
    {
        if (@event is Scalar scalar)
        {
            EmittedValue = scalar.Value;
        }
    }
}
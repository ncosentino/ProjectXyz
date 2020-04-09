using System.Diagnostics;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    [DebuggerDisplay("Not({Wrapped})")]
    public sealed class NotGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public NotGeneratorAttributeValue(IGeneratorAttributeValue wrapped)
        {
            Wrapped = wrapped;
        }

        public IGeneratorAttributeValue Wrapped { get; }
    }
}
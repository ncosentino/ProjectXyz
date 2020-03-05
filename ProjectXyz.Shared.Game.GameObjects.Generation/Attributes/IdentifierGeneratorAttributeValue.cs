using System.Diagnostics;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    [DebuggerDisplay("{Value}")]
    public sealed class IdentifierGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public IdentifierGeneratorAttributeValue(IIdentifier value)
        {
            Value = value;
        }

        public IIdentifier Value { get; }
    }
}
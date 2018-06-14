using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes
{
    public sealed class IdentifierItemGeneratorAttributeValue : IItemGeneratorAttributeValue
    {
        public IdentifierItemGeneratorAttributeValue(IIdentifier value)
        {
            Value = value;
        }

        public IIdentifier Value { get; }
    }
}
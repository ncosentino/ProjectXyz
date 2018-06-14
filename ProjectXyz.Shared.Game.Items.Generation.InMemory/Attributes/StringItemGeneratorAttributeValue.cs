using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes
{
    public sealed class StringItemGeneratorAttributeValue : IItemGeneratorAttributeValue
    {
        public StringItemGeneratorAttributeValue(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
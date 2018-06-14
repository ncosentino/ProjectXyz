using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes
{
    public sealed class DoubleItemGeneratorAttributeValue : IItemGeneratorAttributeValue
    {
        public DoubleItemGeneratorAttributeValue(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}
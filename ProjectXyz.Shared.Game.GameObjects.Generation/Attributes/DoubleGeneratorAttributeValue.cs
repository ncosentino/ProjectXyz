using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class DoubleGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public DoubleGeneratorAttributeValue(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}
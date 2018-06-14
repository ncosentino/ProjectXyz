using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes
{
    public sealed class RangeItemGeneratorAttributeValue : IItemGeneratorAttributeValue
    {
        public RangeItemGeneratorAttributeValue(double minimum, double maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public double Minimum { get; }

        public double Maximum { get; }
    }
}
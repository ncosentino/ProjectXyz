using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes
{
    public sealed class RangeFilterAttributeValue : IFilterAttributeValue
    {
        public RangeFilterAttributeValue(double minimum, double maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public double Minimum { get; }

        public double Maximum { get; }

        public override string ToString() =>
            $"Range ({Minimum}d, {Maximum}d)";
    }
}
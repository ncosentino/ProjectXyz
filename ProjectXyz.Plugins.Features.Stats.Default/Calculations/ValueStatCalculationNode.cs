using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Plugins.Features.Stats.Default.Calculations
{
    public sealed class ValueStatCalculationNode : IStatCalculationNode
    {
        private readonly double _value;

        public ValueStatCalculationNode(double value)
        {
            _value = value;
        }

        public double GetValue()
        {
            return _value;
        }
    }
}
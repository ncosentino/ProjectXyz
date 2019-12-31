using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Plugins.Stats.Calculations
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
using ProjectXyz.Application.Interface.Stats.Calculations;

namespace ProjectXyz.Application.Core.Stats.Calculations
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
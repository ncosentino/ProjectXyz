using ProjectXyz.Application.Stats.Interface.Calculations;

namespace ProjectXyz.Application.Stats.Core.Calculations
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
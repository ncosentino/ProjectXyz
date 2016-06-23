using ProjectXyz.Application.Interface.Stats.Calculations;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StringExpressionEvaluatorWrapper : IStringExpressionEvaluator
    {
        private readonly Framework.Interface.Math.IStringExpressionEvaluator _wrapped;
        private readonly bool _takeOwnership;

        public StringExpressionEvaluatorWrapper(
            Framework.Interface.Math.IStringExpressionEvaluator wrapped,
            bool takeOwnership)
        {
            _takeOwnership = takeOwnership;
            _wrapped = wrapped;
        }

        public double Evaluate(string expression)
        {
            return _wrapped.Evaluate(expression);
        }

        public void Dispose()
        {
            if (_takeOwnership)
            {
                _wrapped?.Dispose();
            }
        }
    }
}

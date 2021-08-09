using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Plugins.Features.Stats.Default.Calculations
{
    public sealed class StringExpressionEvaluatorWrapper : IStringExpressionEvaluator
    {
        private readonly Api.Framework.Math.IStringExpressionEvaluator _wrapped;
        private readonly bool _takeOwnership;

        public StringExpressionEvaluatorWrapper(
            Api.Framework.Math.IStringExpressionEvaluator wrapped,
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

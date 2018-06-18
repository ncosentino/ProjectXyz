using System;
using ProjectXyz.Api.Framework.Math;

namespace ProjectXyz.Shared.Framework.Math
{
    public sealed class GenericExpressionEvaluator : IStringExpressionEvaluator
    {
        #region Fields
        private readonly Func<string, double> _calculateCallback;
        private readonly Action _disposeCallback;
        #endregion

        #region Constructors
        public GenericExpressionEvaluator(Func<string, double> calculateCallback)
            : this(calculateCallback, null)
        {
        }

        public GenericExpressionEvaluator(
            Func<string, double> calculateCallback,
            Action disposeCallback)
        {
            _calculateCallback = calculateCallback;
            _disposeCallback = disposeCallback;
        }
        #endregion

        #region Methods
        public double Evaluate(string expression)
        {
            try
            {
                var value = _calculateCallback(expression);
                return value;
            }
            catch (Exception ex)
            {
                throw new FormatException($"The expression '{expression}' was in an invalid format and could not be evaluated.", ex);
            }
        }

        public void Dispose()
        {
            if (_disposeCallback != null)
            {
                _disposeCallback();
            }
        }
        #endregion
    }
}

using System;
using ProjectXyz.Framework.Interface.Math;

namespace ProjectXyz.Framework.Shared.Math
{
    public sealed class GenericExpressionEvaluator : IStringExpressionEvaluator
    {
        #region Fields
        private readonly Func<string, double> _calculateCallback;
        #endregion

        #region Constructors
        public GenericExpressionEvaluator(Func<string, double> calculateCallback)
        {
            _calculateCallback = calculateCallback;
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
                throw new FormatException($"The expression '{expression}' was in an invalid format and could not be evaulated.", ex);
            }
        }

        public void Dispose()
        {
        }
        #endregion
    }
}

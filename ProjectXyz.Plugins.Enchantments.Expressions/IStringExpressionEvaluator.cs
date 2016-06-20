using System;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        #region Methods
        double Evaluate(string expression);
        #endregion
    }
}

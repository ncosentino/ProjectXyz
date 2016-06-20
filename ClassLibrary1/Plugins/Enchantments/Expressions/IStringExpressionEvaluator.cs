using System;

namespace ClassLibrary1.Plugins.Enchantments.Expressions
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        #region Methods
        double Evaluate(string expression);
        #endregion
    }
}

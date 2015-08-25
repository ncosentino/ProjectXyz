using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IStringExpressionEvaluator : IDisposable
    {
        #region Methods
        double Evaluate(string expression);
        #endregion
    }
}

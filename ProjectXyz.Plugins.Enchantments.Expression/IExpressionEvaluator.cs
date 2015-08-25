using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEvaluator
    {
        #region Methods
        double Evaluate(IExpressionEnchantment expressionEnchantment, IStatCollection stats);
        #endregion
    }
}

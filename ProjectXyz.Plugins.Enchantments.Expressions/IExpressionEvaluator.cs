using System.Collections;
using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public interface IExpressionEvaluator
    {
        #region Methods
        double Evaluate(
            IExpressionEnchantment expressionEnchantment, 
            IReadOnlyDictionary<IIdentifier, IStat> stats);
        #endregion
    }
}

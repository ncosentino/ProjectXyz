using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public interface IExpressionEvaluator
    {
        #region Methods
        double Evaluate(
            IExpressionEnchantment expressionEnchantment, 
            IStatCollection stats);
        #endregion
    }
}

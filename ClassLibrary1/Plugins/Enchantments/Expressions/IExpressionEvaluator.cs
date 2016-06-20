using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Plugins.Enchantments.Expressions
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

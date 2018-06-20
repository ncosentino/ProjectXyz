using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations
{
    public sealed class EnchantmentExpressionBehavior :
        BaseBehavior,
        IEnchantmentExpressionBehavior
    {
        #region Constructors
        public EnchantmentExpressionBehavior(
            ICalculationPriority calculationPriority,
            string expression)
        {
            CalculationPriority = calculationPriority;
            Expression = expression;
        }
        #endregion

        #region Properties
        public string Expression { get; }

        public ICalculationPriority CalculationPriority { get; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"'{GetType()}'\r\n\tCalculation Priority: {CalculationPriority}\r\n\tExpression: {Expression}";
        }
        #endregion
    }
}
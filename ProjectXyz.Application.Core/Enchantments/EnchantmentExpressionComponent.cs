using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentExpressionComponent : IEnchantmentExpressionComponent
    {

        public EnchantmentExpressionComponent(
            ICalculationPriority calculationPriority,
            string expression)
        {
            CalculationPriority = calculationPriority;
            Expression = expression;
        }

        public string Expression { get; }

        public ICalculationPriority CalculationPriority { get; }
    }
}
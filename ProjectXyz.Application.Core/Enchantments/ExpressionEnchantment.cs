using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class ExpressionEnchantment : IExpressionEnchantment
    {
        public ExpressionEnchantment(
            IIdentifier statDefinitionId,
            string expression,
            ICalculationPriority calculationPriority)
        {
            StatDefinitionId = statDefinitionId;
            Expression = expression;
            CalculationPriority = calculationPriority;
        }

        public IIdentifier StatDefinitionId { get; }

        public ICalculationPriority CalculationPriority { get; }

        public string Expression { get; }

        public override string ToString()
        {
            return $"Expression Enchantment\r\n\tStat Definition Id={StatDefinitionId}\r\n\tCalculation Priority:{CalculationPriority}\r\n\tExpression={Expression}";
        }
    }
}
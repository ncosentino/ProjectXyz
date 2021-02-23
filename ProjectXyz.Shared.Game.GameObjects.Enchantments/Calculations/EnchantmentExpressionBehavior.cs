using System.Diagnostics;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
{
    [DebuggerDisplay("Priority: {CalculationPriority}, Expression: {Expression}")]
    public sealed class EnchantmentExpressionBehavior :
        BaseBehavior,
        IEnchantmentExpressionBehavior
    {
        public EnchantmentExpressionBehavior(
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
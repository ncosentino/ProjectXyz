using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentExpressionBehavior : IBehavior
    {
        string Expression { get; }

        ICalculationPriority CalculationPriority { get; }
    }
}

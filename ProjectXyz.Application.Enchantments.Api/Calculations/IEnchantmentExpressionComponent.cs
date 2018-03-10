using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentExpressionComponent : IComponent
    {
        string Expression { get; }

        ICalculationPriority CalculationPriority { get; }
    }
}

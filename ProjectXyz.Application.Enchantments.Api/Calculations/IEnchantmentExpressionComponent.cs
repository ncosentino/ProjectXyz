using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Enchantments.Api.Calculations
{
    public interface IEnchantmentExpressionComponent : IComponent
    {
        string Expression { get; }

        ICalculationPriority CalculationPriority { get; }
    }
}

using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentExpressionComponent : IComponent
    {
        string Expression { get; }

        ICalculationPriority CalculationPriority { get; }
    }
}

using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantment
    {
        IIdentifier StatDefinitionId { get; }

        ICalculationPriority CalculationPriority { get; }
    }
}
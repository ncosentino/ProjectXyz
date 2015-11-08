using ProjectXyz.Application.Interface.Enchantments.Calculations;

namespace ProjectXyz.Application.Interface.GameObjects.Actors
{
    public interface IActorContext
    {
        #region Properties
        IEnchantmentCalculator EnchantmentCalculator { get; }
        #endregion
    }
}

using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments
{
    public interface IHasEnchantmentsBehaviorFactory
    {
        IHasEnchantmentsBehavior Create();

        IHasEnchantmentsBehavior Create(IActiveEnchantmentManager activeEnchantmentManager);
    }
}
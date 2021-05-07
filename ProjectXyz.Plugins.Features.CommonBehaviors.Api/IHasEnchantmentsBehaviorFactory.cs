using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasEnchantmentsBehaviorFactory
    {
        IHasEnchantmentsBehavior Create();

        IHasEnchantmentsBehavior Create(IActiveEnchantmentManager activeEnchantmentManager);
    }
}
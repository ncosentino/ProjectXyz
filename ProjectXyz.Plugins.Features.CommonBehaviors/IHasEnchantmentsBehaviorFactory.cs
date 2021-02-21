using ProjectXyz.Api.Enchantments;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public interface IHasEnchantmentsBehaviorFactory
    {
        IHasEnchantmentsBehavior Create();
        IHasEnchantmentsBehavior Create(IActiveEnchantmentManager activeEnchantmentManager);
    }
}
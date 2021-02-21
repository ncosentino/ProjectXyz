using ProjectXyz.Api.Enchantments;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasEnchantmentsBehaviorFactory : IHasEnchantmentsBehaviorFactory
    {
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;

        public HasEnchantmentsBehaviorFactory(IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory)
        {
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
        }

        public IHasEnchantmentsBehavior Create()
        {
            var activeEnchantmentManager = _activeEnchantmentManagerFactory.Create();
            var behavior = Create(activeEnchantmentManager);
            return behavior;
        }

        public IHasEnchantmentsBehavior Create(IActiveEnchantmentManager activeEnchantmentManager)
        {
            var behavior = new HasEnchantmentsBehavior(activeEnchantmentManager);
            return behavior;
        }
    }
}

using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
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

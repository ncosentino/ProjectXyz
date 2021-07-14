using System;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Triggering;
using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
{
    public sealed class ActiveEnchantmentManagerFactory : IActiveEnchantmentManagerFactory
    {
        private readonly Lazy<ITriggerMechanicRegistrarFacade> _lazyTriggerMechanicRegistrarFacade;
        private readonly Lazy<IEnchantmentTriggerMechanicFactoryFacade> _lazyEnchantmentTriggerMechanicFactoryFacade;

        public ActiveEnchantmentManagerFactory(
            Lazy<ITriggerMechanicRegistrarFacade> lazyTriggerMechanicRegistrarFacade,
            Lazy<IEnchantmentTriggerMechanicFactoryFacade> lazyEnchantmentTriggerMechanicFactoryFacade)
        {
            _lazyTriggerMechanicRegistrarFacade = lazyTriggerMechanicRegistrarFacade;
            _lazyEnchantmentTriggerMechanicFactoryFacade = lazyEnchantmentTriggerMechanicFactoryFacade;
        }

        public IActiveEnchantmentManager Create()
        {
            return new ActiveEnchantmentManager(
                _lazyTriggerMechanicRegistrarFacade.Value,
                _lazyEnchantmentTriggerMechanicFactoryFacade.Value);
        }
    }
}
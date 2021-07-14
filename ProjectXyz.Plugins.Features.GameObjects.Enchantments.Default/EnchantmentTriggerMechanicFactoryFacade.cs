using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Triggering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
{
    public sealed class EnchantmentTriggerMechanicFactoryFacade : IEnchantmentTriggerMechanicFactoryFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableEnchantmentTriggerMechanicFactory>> _lazyDiscoverableEnchantmentTriggerMechanicFactories;

        public EnchantmentTriggerMechanicFactoryFacade(Lazy<IEnumerable<IDiscoverableEnchantmentTriggerMechanicFactory>> lazyDiscoverableEnchantmentTriggerMechanicFactories)
        {
            _lazyDiscoverableEnchantmentTriggerMechanicFactories = new Lazy<IReadOnlyCollection<IDiscoverableEnchantmentTriggerMechanicFactory>>(() =>
                lazyDiscoverableEnchantmentTriggerMechanicFactories.Value.ToArray());
        }

        public IEnumerable<ITriggerMechanic> CreateTriggerMechanicsForEnchantment(
            IGameObject enchantment,
            RemoveTriggerMechanicDelegate removeTriggerMechanicCallback,
            RemoveEnchantmentDelegate removeEnchantmentCallback)
        {
            var results = _lazyDiscoverableEnchantmentTriggerMechanicFactories
                .Value
                .SelectMany(x => x.CreateTriggerMechanicsForEnchantment(
                    enchantment,
                    removeTriggerMechanicCallback,
                    removeEnchantmentCallback));
            return results;
        }
    }
}
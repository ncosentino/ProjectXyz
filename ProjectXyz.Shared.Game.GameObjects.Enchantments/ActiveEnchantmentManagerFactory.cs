using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.ExpiringEnchantments.Api;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments
{
    public sealed class ActiveEnchantmentManagerFactory : IActiveEnchantmentManagerFactory
    {
        private readonly ITriggerMechanicRegistrar _triggerMechanicRegistrar;
        private readonly IReadOnlyCollection<IEnchantmentTriggerMechanicRegistrar> _enchantmentTriggerMechanicRegistrars;

        public ActiveEnchantmentManagerFactory(
            ITriggerMechanicRegistrar triggerMechanicRegistrar,
            IEnumerable<IEnchantmentTriggerMechanicRegistrar> enchantmentTriggerMechanicRegistrars)
        {
            _triggerMechanicRegistrar = triggerMechanicRegistrar;;
            _enchantmentTriggerMechanicRegistrars = enchantmentTriggerMechanicRegistrars.ToArray();
        }

        public IActiveEnchantmentManager Create()
        {
            return new ActiveEnchantmentManager(
                _triggerMechanicRegistrar,
                _enchantmentTriggerMechanicRegistrars);
        }
    }
}
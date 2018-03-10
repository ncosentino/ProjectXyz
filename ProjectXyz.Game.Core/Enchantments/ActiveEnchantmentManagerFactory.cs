using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Enchantments;
using ProjectXyz.Game.Interface.Enchantments;

namespace ProjectXyz.Game.Core.Enchantments
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
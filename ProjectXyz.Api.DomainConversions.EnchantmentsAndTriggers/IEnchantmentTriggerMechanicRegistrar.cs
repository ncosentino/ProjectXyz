using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers
{
    public interface IEnchantmentTriggerMechanicRegistrar
    {
        IEnumerable<ITriggerMechanic> RegisterToEnchantment(
            IEnchantment enchantment,
            RemoveTriggerMechanicDelegate removeTriggerMechanicCallback);
    }
}
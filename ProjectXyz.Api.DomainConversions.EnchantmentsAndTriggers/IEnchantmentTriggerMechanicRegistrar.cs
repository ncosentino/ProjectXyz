using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Api.Triggering.Enchantments
{
    public interface IEnchantmentTriggerMechanicRegistrar
    {
        IEnumerable<ITriggerMechanic> RegisterToEnchantment(
            IEnchantment enchantment,
            RemoveTriggerMechanicDelegate removeTriggerMechanicCallback);
    }
}
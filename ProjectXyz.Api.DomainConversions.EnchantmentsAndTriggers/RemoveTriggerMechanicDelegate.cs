using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Api.DomainConversions.EnchantmentsAndTriggers
{
    public delegate void RemoveTriggerMechanicDelegate(
        IEnchantment enchantment,
        ITriggerMechanic triggerMechanic);
}
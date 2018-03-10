using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Api.Triggering.Enchantments
{
    public delegate void RemoveTriggerMechanicDelegate(
        IEnchantment enchantment,
        ITriggerMechanic triggerMechanic);
}
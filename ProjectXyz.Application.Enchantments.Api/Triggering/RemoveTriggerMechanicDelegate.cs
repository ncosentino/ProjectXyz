using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Api.Enchantments.Triggering
{
    public delegate void RemoveTriggerMechanicDelegate(
        IEnchantment enchantment,
        ITriggerMechanic triggerMechanic);
}
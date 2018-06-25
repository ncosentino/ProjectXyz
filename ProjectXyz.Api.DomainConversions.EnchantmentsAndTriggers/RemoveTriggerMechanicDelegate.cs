using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.ExpiringEnchantments.Api
{
    public delegate void RemoveTriggerMechanicDelegate(
        IEnchantment enchantment,
        ITriggerMechanic triggerMechanic);
}
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Api.Enchantments.Triggering
{
    public delegate void RemoveTriggerMechanicDelegate(
        IGameObject enchantment,
        ITriggerMechanic triggerMechanic);
}
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Api.Enchantments.Triggering
{
    public delegate void RemoveTriggerMechanicDelegate(
        IGameObject enchantment,
        ITriggerMechanic triggerMechanic);
}
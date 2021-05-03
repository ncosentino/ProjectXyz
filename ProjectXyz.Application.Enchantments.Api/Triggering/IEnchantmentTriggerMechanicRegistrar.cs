using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Api.Enchantments.Triggering
{
    public interface IEnchantmentTriggerMechanicRegistrar
    {
        IEnumerable<ITriggerMechanic> RegisterToEnchantment(
            IGameObject enchantment,
            RemoveTriggerMechanicDelegate removeTriggerMechanicCallback);
    }
}
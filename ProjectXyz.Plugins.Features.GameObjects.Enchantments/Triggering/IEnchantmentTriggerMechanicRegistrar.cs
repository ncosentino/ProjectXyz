using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Api.Enchantments.Triggering
{
    public interface IEnchantmentTriggerMechanicFactory
    {
        IEnumerable<ITriggerMechanic> CreateTriggerMechanicsForEnchantment(
            IGameObject enchantment,
            RemoveTriggerMechanicDelegate removeTriggerMechanicCallback,
            RemoveEnchantmentDelegate removeEnchantmentCallback);
    }
}
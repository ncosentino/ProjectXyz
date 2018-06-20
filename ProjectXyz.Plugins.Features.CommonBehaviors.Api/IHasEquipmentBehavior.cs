using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasEquipmentBehavior : IBehavior
    {
        bool TryGet(
            IIdentifier equipSlotId,
            out ICanBeEquippedBehavior canBeEquipped);
    }
}
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IHasEquipmentBehavior : IBehavior
    {
        bool TryGet(
            IIdentifier equipSlotId,
            out ICanBeEquippedBehavior canBeEquipped);
    }
}
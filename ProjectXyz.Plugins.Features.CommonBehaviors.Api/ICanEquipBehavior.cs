using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ICanEquipBehavior : IObservableHasEquipmentBehavior
    {
        bool TryUnequip(
            IIdentifier equipSlotId,
            out ICanBeEquippedBehavior canBeEquipped);

        bool CanEquip(
            IIdentifier equipSlotId,
            ICanBeEquippedBehavior canBeEquipped);

        bool TryEquip(
            IIdentifier equipSlotId,
            ICanBeEquippedBehavior canBeEquipped);
    }
}
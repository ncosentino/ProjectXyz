using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ICanEquipBehavior : IObservableHasEquipmentBehavior
    {
        bool TryUnequip(
            IIdentifier equipSlotId,
            out ICanBeEquippedBehavior canBeEquipped);

        bool CanEquip(
            IIdentifier equipSlotId,
            ICanBeEquippedBehavior canBeEquipped,
            bool allowSwap);

        bool TryEquip(
            IIdentifier equipSlotId,
            ICanBeEquippedBehavior canBeEquipped,
            bool allowSwap);
    }

    public static class ICanEquipBehaviorExtensions
    {
        public static bool TryEquip(
            this ICanEquipBehavior canEquipBehavior,
            IIdentifier equipSlotId,
            IGameObject item,
            bool allowSwap)
        {
            if (!item.TryGetFirst<ICanBeEquippedBehavior>(out var canBeEquippedBehavior))
            {
                return false;
            }

            return canEquipBehavior.TryEquip(
                equipSlotId,
                canBeEquippedBehavior,
                allowSwap);
        }

        public static bool CanEquip(
            this ICanEquipBehavior canEquipBehavior,
            IIdentifier equipSlotId,
            IGameObject item,
            bool allowSwap)
        {
            if (!item.TryGetFirst<ICanBeEquippedBehavior>(out var canBeEquippedBehavior))
            {
                return false;
            }

            return canEquipBehavior.CanEquip(
                equipSlotId,
                canBeEquippedBehavior,
                allowSwap);
        }
    }
}
using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface ICanEquipBehavior : IHasEquipmentBehavior
    {
        event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>>> Equipped;

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
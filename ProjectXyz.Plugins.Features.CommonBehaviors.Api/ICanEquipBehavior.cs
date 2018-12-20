using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ICanEquipBehavior : IHasEquipmentBehavior
    {
        event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>>> Equipped;

        event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>>> Unequipped;

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
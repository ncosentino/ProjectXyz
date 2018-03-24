using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class CanEquipBehavior :
        BaseBehavior,
        ICanEquipBehavior
    {
        private readonly Dictionary<IIdentifier, ICanBeEquippedBehavior> _equipment;

        public CanEquipBehavior()
        {
            _equipment = new Dictionary<IIdentifier, ICanBeEquippedBehavior>();
        }

        public event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>>> Equipped;

        public bool TryUnequip(
            IIdentifier equipSlotId,
            out ICanBeEquippedBehavior canBeEquipped)
        {
            return _equipment.TryGetValue(
                       equipSlotId,
                       out canBeEquipped) &&
                   _equipment.Remove(equipSlotId);
        }

        public bool TryGet(
            IIdentifier equipSlotId,
            out ICanBeEquippedBehavior canBeEquipped)
        {
            return _equipment.TryGetValue(
                equipSlotId,
                out canBeEquipped);
        }

        public bool CanEquip(
            IIdentifier equipSlotId,
            ICanBeEquippedBehavior canBeEquipped)
        {
            // TODO: check all the requirements...
            return _equipment.ContainsKey(equipSlotId);
        }

        public bool TryEquip(
            IIdentifier equipSlotId,
            ICanBeEquippedBehavior canBeEquipped)
        {
            if (CanEquip(
                equipSlotId,
                canBeEquipped))
            {
                return false;
            }

            _equipment[equipSlotId] = canBeEquipped;

            Equipped?.Invoke(
                this,
                new EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>>(new Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>(
                    this,
                    canBeEquipped)));
            return true;
        }
    }
}
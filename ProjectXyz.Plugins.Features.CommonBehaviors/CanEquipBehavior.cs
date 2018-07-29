using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
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

        public event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>>> Unequipped;

        public bool TryUnequip(
            IIdentifier equipSlotId,
            out ICanBeEquippedBehavior canBeEquipped)
        {
            if (!_equipment.TryGetValue(
                equipSlotId,
                out canBeEquipped))
            {
                return false;
            }

            if (!_equipment.Remove(equipSlotId))
            {
                return false;
            }

            Unequipped?.Invoke(
                this,
                new EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>>(new Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>(
                    this,
                    canBeEquipped)));
            return true;
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
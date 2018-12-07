using System;
using System.Collections.Generic;
using System.Linq;
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

        public CanEquipBehavior(IEnumerable<IIdentifier> supportedEquipSlotIds)
        {
            _equipment = new Dictionary<IIdentifier, ICanBeEquippedBehavior>();
            SupportedEquipSlotIds = supportedEquipSlotIds.ToArray();
        }

        public event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>>> Equipped;

        public event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>>> Unequipped;

        public IReadOnlyCollection<IIdentifier> SupportedEquipSlotIds { get; }

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
            // already occupied
            if (_equipment.ContainsKey(equipSlotId))
            {
                return false;
            }

            // does our set of equipment support the asked-for slot?
            if (!SupportedEquipSlotIds.Contains(equipSlotId))
            {
                return false;
            }

            // not even valid based on the item!
            if (!canBeEquipped.AllowedEquipSlots.Contains(equipSlotId))
            {
                return false;
            }

            // TODO: check all the requirements...
            return true;
        }

        public bool TryEquip(
            IIdentifier equipSlotId,
            ICanBeEquippedBehavior canBeEquipped)
        {
            if (!CanEquip(
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
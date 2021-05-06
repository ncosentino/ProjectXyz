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
            : this(
                  supportedEquipSlotIds,
                  new Dictionary<IIdentifier, ICanBeEquippedBehavior>())
        {
        }

        public CanEquipBehavior(
            IEnumerable<IIdentifier> supportedEquipSlotIds,
            IEnumerable<KeyValuePair<IIdentifier, ICanBeEquippedBehavior>> equipment)
        {
            _equipment = equipment.ToDictionary(x => x.Key, x => x.Value);
            SupportedEquipSlotIds = supportedEquipSlotIds.ToArray();
        }

        public event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>>> Equipped;

        public event EventHandler<EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>>> Unequipped;

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
                new EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>>(new Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>(
                    this,
                    canBeEquipped,
                    equipSlotId)));
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
            ICanBeEquippedBehavior canBeEquipped,
            bool allowSwap)
        {
            // already occupied
            if (!allowSwap && _equipment.ContainsKey(equipSlotId))
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
            ICanBeEquippedBehavior canBeEquipped,
            bool allowSwap)
        {
            if (!CanEquip(
                equipSlotId,
                canBeEquipped,
                allowSwap))
            {
                return false;
            }

            _equipment[equipSlotId] = canBeEquipped;

            Equipped?.Invoke(
                this,
                new EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>>(new Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>(
                    this,
                    canBeEquipped,
                    equipSlotId)));
            return true;
        }
    }
}
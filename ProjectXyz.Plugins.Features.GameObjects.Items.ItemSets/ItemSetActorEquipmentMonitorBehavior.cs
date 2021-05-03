using System;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class ItemSetActorEquipmentMonitorBehavior :
        BaseBehavior,
        IItemSetActorEquipmentMonitorBehavior
    {
        private readonly IItemSetManager _itemSetManager;

        public ItemSetActorEquipmentMonitorBehavior(IItemSetManager itemSetManager)
        {
            _itemSetManager = itemSetManager;
        }

        private IObservableHasEquipmentBehavior _observableHasEquipmentBehavior;

        protected override void OnRegisteredToOwner(IGameObject owner)
        {
            base.OnRegisteredToOwner(owner);

            owner
                .Behaviors
                .TryGetFirst(out IObservableHasEquipmentBehavior observableHasEquipmentBehavior);
            if (_observableHasEquipmentBehavior == observableHasEquipmentBehavior)
            {
                return;
            }

            if (_observableHasEquipmentBehavior != null)
            {
                _observableHasEquipmentBehavior.Equipped -= ObservableHasEquipmentBehavior_Equipped;
                _observableHasEquipmentBehavior.Unequipped -= ObservableHasEquipmentBehavior_Unequipped;
            }

            _observableHasEquipmentBehavior = observableHasEquipmentBehavior;

            if (_observableHasEquipmentBehavior != null)
            {
                _observableHasEquipmentBehavior.Equipped += ObservableHasEquipmentBehavior_Equipped;
                _observableHasEquipmentBehavior.Unequipped += ObservableHasEquipmentBehavior_Unequipped;
            }
        }

        private void ObservableHasEquipmentBehavior_Unequipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>> e)
        {
            Refresh();
        }

        private void ObservableHasEquipmentBehavior_Equipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>> e)
        {
            Refresh();
        }

        private void Refresh()
        {
            _itemSetManager.UpdateItemSets(Owner);
        }
    }
}

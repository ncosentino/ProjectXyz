using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class ItemSetActorInventoryMonitorBehavior :
        BaseBehavior,
        IItemSetActorInventoryMonitorBehavior
    {
        private readonly IItemSetManager _itemSetManager;
        private readonly HashSet<IObservableItemContainerBehavior> _observableItemContainerBehaviors;

        public ItemSetActorInventoryMonitorBehavior(IItemSetManager itemSetManager)
        {
            _observableItemContainerBehaviors = new HashSet<IObservableItemContainerBehavior>();
            _itemSetManager = itemSetManager;
        }

        protected override void OnRegisteredToOwner(IHasBehaviors owner)
        {
            base.OnRegisteredToOwner(owner);

            var observableItemContainerBehaviors = owner
                .Behaviors
                .GetOnly<IHasItemContainersBehavior>()
                .ItemContainers;

            var missingContainers = _observableItemContainerBehaviors.Except(observableItemContainerBehaviors);
            foreach (var missing in missingContainers)
            {
                missing.ItemsChanged -= ObservableItemContainerBehavior_ItemsChanged;
                _observableItemContainerBehaviors.Remove(missing);
            }

            var newContainers = observableItemContainerBehaviors.Except(_observableItemContainerBehaviors);
            foreach (var newContainer in newContainers)
            {
                newContainer.ItemsChanged += ObservableItemContainerBehavior_ItemsChanged;
                _observableItemContainerBehaviors.Add(newContainer);
            }
        }

        private void ObservableItemContainerBehavior_ItemsChanged(
            object sender,
            ItemsChangedEventArgs e)
        {
            _itemSetManager.UpdateItemSets((IGameObject)Owner); // FIXME: wtf is this shitty casting...
        }
    }
}

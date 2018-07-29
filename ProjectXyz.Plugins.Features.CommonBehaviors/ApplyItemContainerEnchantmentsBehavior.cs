using System;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    [Obsolete(
        "Instead of having an event driven system to try and keep disjoint " +
        "state synchronized, I think it might better to try and create an on-" +
        "demand system where someone can ask for all applicable enchantments. " +
        "That filtering mechanism can be extended and customized, and maybe " +
        "this event system can be used for cache invalidation or something.")]
    public sealed class ApplyItemContainerEnchantmentsBehavior :
        BaseBehavior,
        IApplyItemContainerEnchantmentsBehavior
    {
        private IObservableItemContainerBehavior _itemContainerBehavior;

        protected override void OnRegisteredToOwner(IHasBehaviors owner)
        {
            base.OnRegisteredToOwner(owner);

            if (owner.Behaviors.TryGetFirst(out _itemContainerBehavior))
            {
                _itemContainerBehavior.ItemsChanged += ItemContainerBehavior_ItemsChanged;
            }
        }

        private void ItemContainerBehavior_ItemsChanged(
            object sender,
            ItemsChangedEventArgs e)
        {
            var owner = _itemContainerBehavior.Owner;

            IBuffableBehavior buffable;
            IHasEnchantmentsBehavior hasEnchantments;
            if (!owner.Behaviors.TryGetFirst(out buffable) ||
                !owner.Behaviors.TryGetFirst(out hasEnchantments))
            {
                return;
            }

            // TODO: filter these by a behavior that allows enchantments to
            // be applied while in corresponding item container
            ////var removedEnchantments = e
            ////    .RemovedItems
            ////    .SelectMany(x => x.Get<IHasEnchantmentsBehavior>())
            ////    .SelectMany(x => x.Enchantments);
            ////buffable.RemoveEnchantments(removedEnchantments);

            ////var addedEnchantments = e
            ////    .AddedItems
            ////    .SelectMany(x => x.Get<IHasEnchantmentsBehavior>())
            ////    .SelectMany(x => x.Enchantments);
            ////buffable.AddEnchantments(addedEnchantments);
        }
    }
}
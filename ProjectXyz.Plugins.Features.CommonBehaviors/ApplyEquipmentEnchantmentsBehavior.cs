using System;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;
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
    public sealed class ApplyEquipmentEnchantmentsBehavior :
        BaseBehavior,
        IApplyEquipmentEnchantmentsBehavior
    {
        private ICanEquipBehavior _canEquip;

        protected override void OnRegisteredToOwner(IHasBehaviors owner)
        {
            base.OnRegisteredToOwner(owner);

            if (owner.Behaviors.TryGetFirst(out _canEquip))
            {
                _canEquip.Equipped += CanEquip_Equipped;
                _canEquip.Unequipped += CanEquip_Unequipped;
            }
        }

        private void CanEquip_Unequipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>> e)
        {
            if (e.Data.Item1.Owner.Behaviors.TryGetFirst(out IBuffableBehavior buffable) &&
                e.Data.Item2.Owner.Behaviors.TryGetFirst(out IHasEnchantmentsBehavior hasEnchantments))
            {
                buffable.RemoveEnchantments(hasEnchantments.Enchantments);
            }
        }

        private void CanEquip_Equipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior, IIdentifier>> e)
        {
            if (e.Data.Item1.Owner.Behaviors.TryGetFirst(out IBuffableBehavior buffable) &&
                e.Data.Item2.Owner.Behaviors.TryGetFirst(out IHasEnchantmentsBehavior hasEnchantments))
            {
                buffable.AddEnchantments(hasEnchantments.Enchantments);
            }
        }
    }
}
using System;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
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
            }
        }

        private void CanEquip_Equipped(
            object sender,
            EventArgs<Tuple<ICanEquipBehavior, ICanBeEquippedBehavior>> e)
        {
            IBuffableBehavior buffable;
            IHasEnchantmentsBehavior hasEnchantments;
            if (e.Data.Item1.Owner.Behaviors.TryGetFirst(out buffable) &&
                e.Data.Item2.Owner.Behaviors.TryGetFirst(out hasEnchantments))
            {
                buffable.AddEnchantments(hasEnchantments.Enchantments);
            }
        }
    }
}
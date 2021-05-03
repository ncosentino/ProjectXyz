using System;

using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class ApplySocketEnchantmentsBehavior :
        BaseBehavior,
        IApplySocketEnchantmentsBehavior
    {
        private ICanBeSocketedBehavior _canBeSocketed;

        protected override void OnRegisteredToOwner(IGameObject owner)
        {
            base.OnRegisteredToOwner(owner);

            owner
                .Behaviors
                .TryGetFirst(out ICanBeSocketedBehavior newCanBeSocketed);
            if (_canBeSocketed == newCanBeSocketed)
            {
                return;
            }

            if (_canBeSocketed != null)
            {
                _canBeSocketed.Socketed -= CanBeSocketed_Socketed;
            }

            _canBeSocketed = newCanBeSocketed;

            if (_canBeSocketed != null)
            {
                _canBeSocketed.Socketed += CanBeSocketed_Socketed;
            }
        }

        private void CanBeSocketed_Socketed(
            object sender,
            EventArgs<Tuple<ICanBeSocketedBehavior, ICanFitSocketBehavior>> e)
        {
            if (e.Data.Item1.Owner.Behaviors.TryGetFirst(out IHasEnchantmentsBehavior canBeSocketedEnchantments) &&
                e.Data.Item2.Owner.Behaviors.TryGetFirst(out IHasReadOnlyEnchantmentsBehavior canFitSocketEnchantments))
            {
                canBeSocketedEnchantments.AddEnchantments(canFitSocketEnchantments.Enchantments);
            }
        }
    }
}
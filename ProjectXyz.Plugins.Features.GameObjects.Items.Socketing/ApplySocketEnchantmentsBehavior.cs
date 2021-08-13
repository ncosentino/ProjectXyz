using System;

using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    // FIXME: this feels like it may need more thought. if enchantments of the
    // CanFitSocketBehavior object are changed, these will not apply to the
    // parent.
    // - is this supposed to be an optimization so that we don't need to go
    //   into the socketed objects each time we ask for the enchantments from
    //   something?
    // - have we considered that if we have 10% damage on a jewel, is that 10%
    //   damage going to the item it's placed in or the overall damage of the
    //   actor? based on the enchantment target path system we use, this could
    //   have different behavior if we ever want to move enchantments between
    //   different game objects (i.e. through some special crafting or something)
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
                e.Data.Item2.Owner.Behaviors.TryGetFirst(out IReadOnlyHasEnchantmentsBehavior canFitSocketEnchantments))
            {
                canBeSocketedEnchantments.AddEnchantmentsAsync(canFitSocketEnchantments.Enchantments);
            }
        }
    }
}
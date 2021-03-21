using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasEchantmentsGameObjectsForBehavior : IDiscoverableGameObjectsForBehavior
    {
        public Type SupportedBehaviorType { get; } = typeof(HasEnchantmentsBehavior);

        public IEnumerable<IGameObject> GetChildren(IBehavior behavior)
        {
            var hasEnchantmentsBehavior = (IHasReadOnlyEnchantmentsBehavior)behavior;
            foreach (var enchantment in hasEnchantmentsBehavior.Enchantments)
            {
                yield return enchantment;
            }
        }
    }
}

using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
{
    public sealed class HasEnchantmentsGameObjectsForBehavior : IDiscoverableGameObjectsForBehavior
    {
        public Type SupportedBehaviorType { get; } = typeof(HasEnchantmentsBehavior);

        public IEnumerable<IGameObject> GetChildren(IBehavior behavior)
        {
            var hasEnchantmentsBehavior = (IReadOnlyHasEnchantmentsBehavior)behavior;
            foreach (var enchantment in hasEnchantmentsBehavior.Enchantments)
            {
                yield return enchantment;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments
{
    public sealed class EnchantmentFactory : IEnchantmentFactory
    {
        public IEnchantment Create(IEnumerable<IBehavior> behaviors)
        {
            var hasStatDefinitionIdBehavior = behaviors
                .TakeTypes<IHasStatDefinitionIdBehavior>()
                .SingleOrDefault();
            if (hasStatDefinitionIdBehavior == null)
            {
                throw new ArgumentException($"Missing required '{typeof(IHasStatDefinitionIdBehavior)}'.");
            }

            var enchantment = new Enchantment(behaviors);
            return enchantment;
        }
    }
}
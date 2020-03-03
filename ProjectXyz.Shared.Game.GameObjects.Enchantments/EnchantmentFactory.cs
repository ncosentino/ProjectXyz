using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Collections;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments
{
    public sealed class EnchantmentFactory : IEnchantmentFactory
    {
        private readonly IBehaviorCollectionFactory _behaviorCollectionFactory;

        public EnchantmentFactory(
            IBehaviorCollectionFactory behaviorCollectionFactory)
        {
            _behaviorCollectionFactory = behaviorCollectionFactory;
        }

        public IEnchantment Create(IEnumerable<IBehavior> behaviors)
        {
            var hasStatDefinitionIdBehavior = behaviors
                .TakeTypes<IHasStatDefinitionIdBehavior>()
                .SingleOrDefault();
            if (hasStatDefinitionIdBehavior == null)
            {
                throw new InvalidOperationException($"Missing required '{typeof(IHasStatDefinitionIdBehavior)}'.");
            }

            var enchantment = new Enchantment(
                _behaviorCollectionFactory,
                hasStatDefinitionIdBehavior.StatDefinitionId,
                behaviors);
            return enchantment;
        }
    }
}
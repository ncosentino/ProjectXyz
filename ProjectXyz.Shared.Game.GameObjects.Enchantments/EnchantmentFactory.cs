using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

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
            // FIXME: this is NOT how we should do this.
            // - ditch IEnchantment... just use a game object
            // - the stat definition id disappears... that should be a behavior
            // - should NOT need to depend on a feature API?
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
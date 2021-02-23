using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
{
    public sealed class EnchantmentFactory : IEnchantmentFactory
    {
        private readonly IBehaviorManager _behaviorManager;

        public EnchantmentFactory(IBehaviorManager behaviorManager)
        {
            _behaviorManager = behaviorManager;
        }

        public IEnchantment Create(IEnumerable<IBehavior> behaviors)
        {
            if (behaviors
                .TakeTypes<IHasStatDefinitionIdBehavior>()
                .SingleOrDefault() == null)
            {
                throw new ArgumentException($"Missing required '{typeof(IHasStatDefinitionIdBehavior)}'.");
            }

            if (behaviors
                .TakeTypes<IEnchantmentTargetBehavior>()
                .SingleOrDefault() == null)
            {
                throw new ArgumentException($"Missing required '{typeof(IEnchantmentTargetBehavior)}'.");
            }

            var enchantment = new Enchantment(behaviors);
            _behaviorManager.Register(enchantment, enchantment.Behaviors);
            return enchantment;
        }
    }
}
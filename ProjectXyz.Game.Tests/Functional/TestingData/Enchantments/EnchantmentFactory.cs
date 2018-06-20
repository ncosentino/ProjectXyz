using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Shared.Game.GameObjects.Enchantments;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Enchantments
{
    public sealed class EnchantmentFactory
    {
        private readonly IBehaviorCollectionFactory _behaviorCollectionFactory;

        public EnchantmentFactory(IBehaviorCollectionFactory behaviorCollectionFactory)
        {
            _behaviorCollectionFactory = behaviorCollectionFactory;
        }

        public IEnchantment CreateExpressionEnchantment(
            IIdentifier statDefinitionId,
            string expression,
            ICalculationPriority calculationPriority,
            params IBehavior[] additionalBehaviors)
        {
            IEnumerable<IBehavior> behaviors = new EnchantmentExpressionBehavior(
                    calculationPriority,
                    expression)
                .Yield();

            if (additionalBehaviors != null)
            {
                behaviors = behaviors.Concat(additionalBehaviors);
            }

            return new Enchantment(
                _behaviorCollectionFactory,
                statDefinitionId,
                behaviors);
        }
    }
}
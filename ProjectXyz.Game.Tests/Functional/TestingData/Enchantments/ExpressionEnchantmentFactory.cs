using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Enchantments
{
    public sealed class ExpressionEnchantmentFactory
    {
        private readonly IEnchantmentFactory _enchantmentFactory;

        public ExpressionEnchantmentFactory(IEnchantmentFactory enchantmentFactory)
        {
            _enchantmentFactory = enchantmentFactory;
        }

        public IEnchantment CreateExpressionEnchantment(
            IIdentifier statDefinitionId,
            string expression,
            ICalculationPriority calculationPriority,
            params IBehavior[] additionalBehaviors)
        {
            IEnumerable<IBehavior> behaviors = new IBehavior[]
            {
                new EnchantmentExpressionBehavior(
                    calculationPriority,
                    expression),
                new HasStatDefinitionIdBehavior()
                {
                    StatDefinitionId = statDefinitionId,
                },
            };

            if (additionalBehaviors != null)
            {
                behaviors = behaviors.Concat(additionalBehaviors);
            }

            if (additionalBehaviors == null ||
                !additionalBehaviors.Any(x => x is IEnchantmentTargetBehavior))
            {
                behaviors = behaviors.AppendSingle(new EnchantmentTargetBehavior(new StringIdentifier("self")));
            }

            var enchantment = _enchantmentFactory.Create(behaviors);
            return enchantment;
        }
    }
}
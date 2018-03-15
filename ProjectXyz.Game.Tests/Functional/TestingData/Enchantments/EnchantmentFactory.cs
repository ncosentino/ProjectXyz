using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Application.Enchantments.Core;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Framework.Extensions.Collections;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Enchantments
{
    public sealed class EnchantmentFactory
    {
        public IEnchantment CreateExpressionEnchantment(
            IIdentifier statDefinitionId,
            string expression,
            ICalculationPriority calculationPriority,
            params IComponent[] additionalComponents)
        {
            IEnumerable<IComponent> components = new EnchantmentExpressionComponent(
                    calculationPriority,
                    expression)
                .Yield();

            if (additionalComponents != null)
            {
                components = components.Concat(additionalComponents);
            }

            return new Enchantment(
                statDefinitionId,
                components);
        }
    }
}
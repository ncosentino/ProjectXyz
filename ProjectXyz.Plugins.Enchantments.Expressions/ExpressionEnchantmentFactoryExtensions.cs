using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public static class ExpressionEnchantmentFactoryExtensions
    {
        #region Methods
        public static IExpressionEnchantment CreateAdditive(
            this IExpressionEnchantmentFactory expressionEnchantmentFactory,
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier weatherGroupingId,
            IIdentifier statDefinitionId,
            double value,
            int calculationPriority)
        {
            return expressionEnchantmentFactory.Create(
                statusTypeId,
                triggerId,
                weatherGroupingId,
                statDefinitionId,
                "stat + value",
                calculationPriority,
                new KeyValuePair<string, IIdentifier>("stat", statDefinitionId).Yield(), 
                new KeyValuePair<string, double>("value", value).Yield());
        }

        public static IExpressionEnchantment CreateMultiplicative(
            this IExpressionEnchantmentFactory expressionEnchantmentFactory,
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier weatherGroupingId,
            IIdentifier statDefinitionId,
            double value,
            int calculationPriority)
        {
            return expressionEnchantmentFactory.Create(
                statusTypeId,
                triggerId,
                weatherGroupingId,
                statDefinitionId,
                "stat * value",
                calculationPriority,
                new KeyValuePair<string, IIdentifier>("stat", statDefinitionId).Yield(),
                new KeyValuePair<string, double>("value", value).Yield());
        }

        public static IExpressionEnchantment CreateAssignment(
            this IExpressionEnchantmentFactory expressionEnchantmentFactory,
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier weatherGroupingId,
            IIdentifier statDefinitionId,
            double value,
            int calculationPriority)
        {
            return expressionEnchantmentFactory.Create(
                statusTypeId,
                triggerId,
                weatherGroupingId,
                statDefinitionId,
                "value",
                calculationPriority,
                Enumerable.Empty<KeyValuePair<string, IIdentifier>>(),
                new KeyValuePair<string, double>("value", value).Yield());
        }
        #endregion
    }
}

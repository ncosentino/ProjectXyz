using System;
using System.Collections.Generic;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Plugins.Enchantments.Expressions
{
    public sealed class ExpressionEnchantmentFactory : IExpressionEnchantmentFactory
    {
        #region Methods
        public IExpressionEnchantment Create(
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier statDefinitionId,
            IIdentifier weatherGroupingId,
            string expression,
            int calculationPriority,
            IEnumerable<KeyValuePair<string, IIdentifier>> expressionStatDefinitionIds,
            IEnumerable<KeyValuePair<string, double>> expressionValues)
        {
            return new ExpressionEnchantment(
                statusTypeId,
                triggerId,
                statDefinitionId,
                weatherGroupingId,
                expression,
                calculationPriority,
                expressionStatDefinitionIds,
                expressionValues);
        }
        #endregion
    }
}

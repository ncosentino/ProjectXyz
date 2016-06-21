using System.Collections.Generic;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public sealed class ExpressionEnchantmentFactory : IExpressionEnchantmentFactory
    {
        #region Methods
        public IExpressionEnchantment Create(
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier weatherGroupingId,
            IIdentifier statDefinitionId,
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

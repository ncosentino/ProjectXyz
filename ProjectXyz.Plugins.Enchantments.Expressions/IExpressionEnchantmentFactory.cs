using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public interface IExpressionEnchantmentFactory
    {
        IExpressionEnchantment Create(
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier statDefinitionId,
            IIdentifier weatherGroupingId,
            string expression,
            int calculationPriority,
            IEnumerable<KeyValuePair<string, IIdentifier>> expressionStatDefinitionIds,
            IEnumerable<KeyValuePair<string, double>> expressionValues);
    }
}
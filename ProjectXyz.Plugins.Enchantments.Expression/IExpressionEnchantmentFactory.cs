using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentFactory
    {
        #region Methods
        IExpressionEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            Guid weatherTypeGroupingId,
            TimeSpan remainingDuration,
            Guid statId,
            string expression,
            int calculationPriority,
            IEnumerable<KeyValuePair<string, Guid>> expressionStatIds,
            IEnumerable<KeyValuePair<string, double>> expressionValues);
        #endregion
    }
}

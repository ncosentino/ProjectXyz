using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentFactory : IExpressionEnchantmentFactory
    {
        #region Constructors
        private ExpressionEnchantmentFactory()
        {
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentFactory Create()
        {
            var factory = new ExpressionEnchantmentFactory();
            return factory;
        }

        public IExpressionEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            Guid weatherTypeGroupingId,
            TimeSpan remainingDuration,
            Guid statId,
            string expression,
            int calculationPriority,
            IEnumerable<KeyValuePair<string, Guid>> expressionStatIds,
            IEnumerable<KeyValuePair<string, double>> expressionValues)
        {
            var enchantment = ExpressionEnchantment.Create(
                id,
                statusTypeId,
                triggerId,
                weatherTypeGroupingId,
                remainingDuration,
                statId,
                expression,
                calculationPriority,
                expressionStatIds,
                expressionValues);
            return enchantment;
        }
        #endregion
    }
}

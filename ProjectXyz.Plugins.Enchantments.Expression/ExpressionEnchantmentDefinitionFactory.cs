using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public class ExpressionEnchantmentDefinitionFactory : IExpressionEnchantmentDefinitionFactory
    {
        #region Constructors
        private ExpressionEnchantmentDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IExpressionEnchantmentDefinitionFactory>() != null);

            return new ExpressionEnchantmentDefinitionFactory();
        }

        public IExpressionEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid enchantmentDefinitionId,
            string expression,
            Guid statId,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            return ExpressionEnchantmentDefinition.Create(
                id,
                enchantmentDefinitionId,
                expression,
                statId,
                minimumDuration,
                maximumDuration);
        }
        #endregion
    }
}

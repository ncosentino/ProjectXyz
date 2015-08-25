using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public class ExpressionEnchantmentStatDefinitionFactory : IExpressionEnchantmentStatDefinitionFactory
    {
        #region Constructors
        private ExpressionEnchantmentStatDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentStatDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStatDefinitionFactory>() != null);

            return new ExpressionEnchantmentStatDefinitionFactory();
        }

        public IExpressionEnchantmentStatDefinition CreateEnchantmentStatDefinition(
            Guid id,
            Guid enchangmentDefinitionId,
            string idForExpression,
            Guid statId)
        {
            return ExpressionEnchantmentStatDefinition.Create(
                id,
                enchangmentDefinitionId,
                idForExpression,
                statId);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public class ExpressionEnchantmentValueDefinitionFactory : IExpressionEnchantmentValueDefinitionFactory
    {
        #region Constructors
        private ExpressionEnchantmentValueDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentValueDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValueDefinitionFactory>() != null);

            return new ExpressionEnchantmentValueDefinitionFactory();
        }

        public IExpressionEnchantmentValueDefinition CreateEnchantmentValueDefinition(
            Guid id,
            Guid enchangmentDefinitionId,
            string idForExpression,
            double minimumValue,
            double maximumValue)
        {
            return ExpressionEnchantmentValueDefinition.Create(
                id,
                enchangmentDefinitionId,
                idForExpression,
                minimumValue,
                maximumValue);
        }
        #endregion
    }
}

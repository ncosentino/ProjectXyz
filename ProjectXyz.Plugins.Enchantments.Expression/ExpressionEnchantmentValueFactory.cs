using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public class ExpressionEnchantmentValueFactory : IExpressionEnchantmentValueFactory
    {
        #region Constructors
        private ExpressionEnchantmentValueFactory()
        {
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentValueFactory Create()
        {
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValueFactory>() != null);

            return new ExpressionEnchantmentValueFactory();
        }

        public IExpressionEnchantmentValue CreateExpressionEnchantmentValue(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            double value)
        {
            return ExpressionEnchantmentValue.Create(
                id,
                expressionEnchantmentId,
                idForExpression,
                value);
        }
        #endregion
    }
}

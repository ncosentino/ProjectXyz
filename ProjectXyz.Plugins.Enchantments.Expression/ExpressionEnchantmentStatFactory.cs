using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public class ExpressionEnchantmentStatFactory : IExpressionEnchantmentStatFactory
    {
        #region Constructors
        private ExpressionEnchantmentStatFactory()
        {
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentStatFactory Create()
        {
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStatFactory>() != null);

            return new ExpressionEnchantmentStatFactory();
        }

        public IExpressionEnchantmentStat CreateExpressionEnchantmentStat(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            Guid statId)
        {
            return ExpressionEnchantmentStat.Create(
                id,
                expressionEnchantmentId,
                idForExpression,
                statId);
        }
        #endregion
    }
}

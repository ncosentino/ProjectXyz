using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public class ExpressionEnchantmentStoreFactory : IExpressionEnchantmentStoreFactory
    {
        #region Constructors
        private ExpressionEnchantmentStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentStoreFactory Create()
        {
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStoreFactory>() != null);

            return new ExpressionEnchantmentStoreFactory();
        }

        public IExpressionEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId,
            Guid expressionId,
            TimeSpan remainingDuration)
        {
            return ExpressionEnchantmentStore.Create(
                id,
                statId,
                expressionId,
                remainingDuration);
        }
        #endregion
    }
}

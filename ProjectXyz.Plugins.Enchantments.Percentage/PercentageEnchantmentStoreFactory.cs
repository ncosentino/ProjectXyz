using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public class PercentageEnchantmentStoreFactory : IPercentageEnchantmentStoreFactory
    {
        #region Constructors
        private PercentageEnchantmentStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IPercentageEnchantmentStoreFactory Create()
        {
            Contract.Ensures(Contract.Result<IPercentageEnchantmentStoreFactory>() != null);

            return new PercentageEnchantmentStoreFactory();
        }

        public IPercentageEnchantmentStore CreateEnchantmentStore(
            Guid id, 
            Guid statId, 
            double value,
            TimeSpan remainingDuration)
        {
            return PercentageEnchantmentStore.Create(
                id,
                statId,
                value,
                remainingDuration);
        }
        #endregion
    }
}

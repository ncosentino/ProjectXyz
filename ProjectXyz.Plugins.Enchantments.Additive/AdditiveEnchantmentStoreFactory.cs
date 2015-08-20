using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public class AdditiveEnchantmentStoreFactory : IAdditiveEnchantmentStoreFactory
    {
        #region Constructors
        private AdditiveEnchantmentStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentStoreFactory Create()
        {
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentStoreFactory>() != null);

            return new AdditiveEnchantmentStoreFactory();
        }

        public IAdditiveEnchantmentStore CreateEnchantmentStore(
            Guid id, 
            Guid statId, 
            double value,
            TimeSpan remainingDuration)
        {
            return AdditiveEnchantmentStore.Create(
                id,
                statId,
                value,
                remainingDuration);
        }
        #endregion
    }
}

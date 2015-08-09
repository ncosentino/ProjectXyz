using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
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
            Guid enchantmentTypeId,
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId, 
            TimeSpan remainingDuration, 
            double value)
        {
            return AdditiveEnchantmentStore.Create(
                id,
                enchantmentTypeId,
                statId,
                triggerId,
                statusTypeId,
                remainingDuration,
                value);
        }
        #endregion
    }
}

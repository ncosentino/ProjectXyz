using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public class AdditiveEnchantmentStoreFactory : IAdditiveEnchantmentStoreFactory
    {
        #region Fields
        private readonly Guid _enchantmentTypeId;
        #endregion

        #region Constructors
        private AdditiveEnchantmentStoreFactory(Guid enchantmentTypeId)
        {
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            _enchantmentTypeId = enchantmentTypeId;
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentStoreFactory Create(Guid enchantmentTypeId)
        {
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentStoreFactory>() != null);

            return new AdditiveEnchantmentStoreFactory(enchantmentTypeId);
        }

        public IAdditiveEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId, 
            TimeSpan remainingDuration, 
            double value)
        {
            return AdditiveEnchantmentStore.Create(
                id,
                _enchantmentTypeId,
                statId,
                triggerId,
                statusTypeId,
                remainingDuration,
                value);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public class PercentageEnchantmentStoreFactory : IPercentageEnchantmentStoreFactory
    {
        #region FIelds
        private readonly Guid _enchantmentTypeId;
        #endregion

        #region Constructors
        private PercentageEnchantmentStoreFactory(Guid enchantmentTypeId)
        {
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);

            _enchantmentTypeId = enchantmentTypeId;
        }
        #endregion

        #region Methods
        public static IPercentageEnchantmentStoreFactory Create(Guid enchantmentTypeId)
        {
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IPercentageEnchantmentStoreFactory>() != null);

            return new PercentageEnchantmentStoreFactory(enchantmentTypeId);
        }

        public IPercentageEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId, 
            TimeSpan remainingDuration, 
            double value)
        {
            return PercentageEnchantmentStore.Create(
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

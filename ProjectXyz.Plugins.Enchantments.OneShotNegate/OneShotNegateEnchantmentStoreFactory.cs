using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public class OneShotNegateEnchantmentStoreFactory : IOneShotNegateEnchantmentStoreFactory
    {
        #region Fields
        private readonly Guid _enchantmentTypeId;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentStoreFactory(Guid enchantmentTypeId)
        {
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);

            _enchantmentTypeId = enchantmentTypeId;
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentStoreFactory Create(Guid enchantmentTypeId)
        {
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentStoreFactory>() != null);

            return new OneShotNegateEnchantmentStoreFactory(enchantmentTypeId);
        }

        public IOneShotNegateEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId)
        {
            return OneShotNegateEnchantmentStore.Create(
                id,
                _enchantmentTypeId,
                statId,
                triggerId,
                statusTypeId);
        }
        #endregion
    }
}

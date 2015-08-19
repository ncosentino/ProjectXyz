using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public class OneShotNegateEnchantmentStoreFactory : IOneShotNegateEnchantmentStoreFactory
    {
        #region Constructors
        private OneShotNegateEnchantmentStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentStoreFactory Create()
        {
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentStoreFactory>() != null);

            return new OneShotNegateEnchantmentStoreFactory();
        }

        public IOneShotNegateEnchantmentStore CreateEnchantmentStore(
            Guid id, 
            Guid statId)
        {
            return OneShotNegateEnchantmentStore.Create(
                id,
                statId);
        }
        #endregion
    }
}

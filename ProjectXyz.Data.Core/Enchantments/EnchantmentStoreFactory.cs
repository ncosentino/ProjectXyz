using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentStoreFactory : IEnchantmentStoreFactory
    {
        #region Constructors
        private EnchantmentStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentStoreFactory Create()
        {
            var factory = new EnchantmentStoreFactory();
            return factory;
        }

        public IEnchantmentStore Create(
            Guid id,
            Guid enchantmentTypeId,
            Guid triggerId,
            Guid statusTypeId,
            Guid weatherTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            var enchantmentStore = EnchantmentStore.Create(
                id,
                enchantmentTypeId,
                triggerId,
                statusTypeId,
                weatherTypeGroupingId);
            return enchantmentStore;
        }
        #endregion
    }
}

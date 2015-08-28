using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemEnchantmentFactory : IItemEnchantmentFactory
    {
        #region Constructors
        private ItemEnchantmentFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemEnchantmentFactory Create()
        {
            var factory = new ItemEnchantmentFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemEnchantment Create(
            Guid id,
            Guid itemId,
            Guid enchantmentId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemEnchantment>() != null);

            var itemEnchantment = ItemEnchantment.Create(
                id,
                itemId,
                enchantmentId);
            return itemEnchantment;
        }
        #endregion
    }
}

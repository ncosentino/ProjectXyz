using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemEnchantment : IItemEnchantment
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemId;
        private readonly Guid _enchantmentId;
        #endregion

        #region Constructors
        private ItemEnchantment(
            Guid id,
            Guid itemId,
            Guid enchantmentId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentId != Guid.Empty);

            _id = id;
            _itemId = itemId;
            _enchantmentId = enchantmentId;
        }
        #endregion

        #region Properties
        public Guid Id { get { return _id; } }

        public Guid ItemId { get { return _itemId; } }

        public Guid EnchantmentId { get { return _enchantmentId; } }
        #endregion

        #region Methods
        public static IItemEnchantment Create(
            Guid id,
            Guid itemId,
            Guid enchantmentId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemEnchantment>() != null);

            var itemEnchantment = new ItemEnchantment(
                id,
                itemId,
                enchantmentId);
            return itemEnchantment;
        }
        #endregion
    }
}

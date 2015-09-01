using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Core.Items.Affixes
{
    public sealed class ItemAffixDefinitionEnchantment : IItemAffixDefinitionEnchantment
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemAffixDefinitionId;
        private readonly Guid _enchantmentDefinitionId;
        #endregion

        #region Constructors
        private ItemAffixDefinitionEnchantment(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemAffixDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);

            _id = id;
            _itemAffixDefinitionId = itemAffixDefinitionId;
            _enchantmentDefinitionId = enchantmentDefinitionId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ItemAffixDefinitionId { get { return _itemAffixDefinitionId; } }

        /// <inheritdoc />
        public Guid EnchantmentDefinitionId { get { return _enchantmentDefinitionId; } }
        #endregion

        #region Methods
        public static IItemAffixDefinitionEnchantment Create(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemAffixDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionEnchantment>() != null);

            var itemAffixDefinitionEnchantment = new ItemAffixDefinitionEnchantment(
                id,
                itemAffixDefinitionId,
                enchantmentDefinitionId);
            return itemAffixDefinitionEnchantment;
        }
        #endregion
    }
}

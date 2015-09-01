using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Core.Items.Affixes
{
    public sealed class ItemAffixDefinitionEnchantmentFactory : IItemAffixDefinitionEnchantmentFactory
    {
        #region Constructors
        private ItemAffixDefinitionEnchantmentFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemAffixDefinitionEnchantmentFactory Create()
        {
            var factory = new ItemAffixDefinitionEnchantmentFactory();
            return factory;
        }

        public IItemAffixDefinitionEnchantment Create(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemAffixDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionEnchantment>() != null);

            var itemAffixDefinitionEnchantment = ItemAffixDefinitionEnchantment.Create(
                id,
                itemAffixDefinitionId,
                enchantmentDefinitionId);
            return itemAffixDefinitionEnchantment;
        }
        #endregion
    }
}

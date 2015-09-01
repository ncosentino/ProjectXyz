using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Core.Items.Affixes
{
    public sealed class ItemAffixDefinitionMagicTypeGroupingFactory : IItemAffixDefinitionMagicTypeGroupingFactory
    {
        #region Constructors
        private ItemAffixDefinitionMagicTypeGroupingFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemAffixDefinitionMagicTypeGroupingFactory Create()
        {
            var factory = new ItemAffixDefinitionMagicTypeGroupingFactory();
            return factory;
        }

        public IItemAffixDefinitionMagicTypeGrouping Create(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid magicTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemAffixDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeGroupingId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionMagicTypeGrouping>() != null);

            var itemAffixDefinitionEnchantment = ItemAffixDefinitionMagicTypeGrouping.Create(
                id,
                itemAffixDefinitionId,
                magicTypeGroupingId);
            return itemAffixDefinitionEnchantment;
        }
        #endregion
    }
}
